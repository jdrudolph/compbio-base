using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using BaseLib.Forms.Base;
using BaseLib.Graphic;

namespace UtilsC.Util{
	public static class ExportGraphics{
		public static void ExportGraphic(Control control, string filename){
			ExportGraphic(control, filename, true);
		}

		public static void ExportGraphic(Control control, string filename, bool showDialog){
			if (control == null){
				return;
			}
			ExportGraphic(control, filename, control.Width, control.Height, showDialog);
		}

		private static string ShowDialog(string filename){
			SaveFileDialog dialog = new SaveFileDialog{
				Filter = "Pdf file (*.pdf)|*.pdf|Svg file (*.svg)|*.svg|PNG file (*.png)|*.png|JPEG file (*.jpg)|*.jpg",
				FileName = filename
			};
			return dialog.ShowDialog() == DialogResult.OK ? dialog.FileName : null;
		}

		private static void ExportGraphic(Control control, string filename, int width, int height, bool showDialog){
			if (showDialog){
				filename = ShowDialog(filename);
			}
			if (filename == null){
				return;
			}
			Stream stream = null;
			IGraphics graphics;
			ImageFormat imageFormat = null;
			string extension = System.IO.Path.GetExtension(filename);
			if (!string.IsNullOrEmpty(extension)){
				extension = extension.ToLower();
			}
			switch (extension){
				case ".svg":
					if (File.Exists(filename)){
						try{
							File.Delete(filename);
						} catch (Exception){
							MessageBox.Show("The file " + filename + " is open in another program.");
							return;
						}
					}
					stream = new FileStream(filename, FileMode.CreateNew);
					graphics = new SvgGraphics(stream, width, height);
					break;
				case ".pdf":
					if (File.Exists(filename)){
						try{
							File.Delete(filename);
						} catch (Exception){
							MessageBox.Show("The file " + filename + " is open in another program.");
							return;
						}
					}
					stream = new FileStream(filename, FileMode.CreateNew);
					graphics = new PdfGraphics(stream, width, height);
					break;
				case ".png":
					imageFormat = ImageFormat.Png;
					Graphics g = control.CreateGraphics();
					g.Clip = new Region(new RectangleF(0, 0, width, height));
					graphics = new CGraphics(g);
					break;
				case ".jpg":
					imageFormat = ImageFormat.Jpeg;
					Graphics g2 = control.CreateGraphics();
					g2.Clip = new Region(new RectangleF(0, 0, width, height));
					graphics = new CGraphics(g2);
					break;
				default:
					MessageBox.Show("Could not find specified fileformat " + extension);
					return;
			}
			control.CreateControl();
			if (graphics is CGraphics){
				CGraphics g = (CGraphics) graphics;
				Bitmap bitmap = new Bitmap(width, height, g.Graphics);
				control.DrawToBitmap(bitmap, new Rectangle(0, 0, width, height));
				bitmap.Save(filename, imageFormat);
				bitmap.Dispose();
				graphics.Dispose();
			} else if (control is BasicControl){
				((BasicControl) control).view.Print(graphics, control.Width, control.Height);
			} else{
				DoPaintBackground(graphics, control);
				DoPaint(graphics, control);
			}
			if (stream != null){
				graphics.Dispose();
				stream.Close();
				stream.Dispose();
			}
		}

		public static void DoPaint(IGraphics g, Control control){
			DoPaint(g, control, 0, 0);
		}

		public static void DoPaint(IGraphics g, Control container, int addx, int addy){
			DoPaintBackground(g, container);
			if (container is Label){
				DoPaint(g, (Label) container);
			} else if (container is TextBox){
				DoPaint(g, (TextBox) container);
			} else if (container is RichTextBox){
				DoPaint(g, (RichTextBox) container);
			} else{
				(container as BaseDrawControl)?.DoPaint(g);
				foreach (Control control in container.Controls){
					g.SetClippingMask(control.Width, control.Height, container.Location.X + control.Location.X + addx,
						container.Location.Y + control.Location.Y + addy);
					// Clipping mask: (50,50) 682x306
					Debug.WriteLine("Clipping mask: (" + (container.Location.X + control.Location.X) + "," +
						(container.Location.Y + control.Location.Y) + ") " + control.Width + "x" + control.Height);
					if (control is TableLayoutPanel){
						Debug.WriteLine("TableLayoutPanel");
						DoPaint(g, control as TableLayoutPanel, container.Location.X + addx, container.Location.Y + addy);
					} else if (control is SplitContainer){
						Debug.WriteLine("SplitContainer");
						DoPaint(g, control as SplitContainer);
					} else if (control is SplitterPanel){
						Debug.WriteLine("SplitterPanel");
						DoPaint(g, control as SplitterPanel);
					} else if (control is TextBox){
						Debug.WriteLine("TextBox");
						DoPaint(g, control as TextBox);
					} else if (control is Label){
						Debug.WriteLine("Label");
						DoPaint(g, control as Label);
					} else if (control is RichTextBox){
						Debug.WriteLine("RichTextBox");
						DoPaint(g, control as RichTextBox);
					} else if (control is DataGridView){
						Debug.WriteLine("DataGridView");
						DoPaint(g, control as DataGridView);
					} else if (control is BasicControl){
						Debug.WriteLine("-" + control.GetType().Name);
						((BasicControl) control).view.Print(g, control.Width, control.Height);
					} else if (control is Panel){
						DoPaint(g, control as Panel, container.Location.X + addx, container.Location.Y + addy);
					} else if (control is PictureBox){
						PictureBox box = control as PictureBox;
						if (box.Image != null){
							g.DrawImage(box.Image, box.Location.X, box.Location.Y, box.Size.Width, box.Size.Height);
						}
					} else{
						DoPaint(g, control);
					}
				}
			}
		}

		internal static void DoPaint(IGraphics g, TableLayoutPanel table){
			DoPaint(g, table as Control);
		}

		internal static void DoPaint(IGraphics g, SplitterPanel container){
			DoPaint(g, container as Control);
		}

		internal static void DoPaint(IGraphics g, SplitContainer container){
			DoPaint(g, container.Panel1);
			DoPaint(g, container.Panel2);
		}

		internal static void DoPaint(IGraphics g, Panel control, int addx, int addy){
			g.FillRectangle(new SolidBrush(control.BackColor), 0, 0, control.Width, control.Height);
			if (control.BorderStyle == BorderStyle.FixedSingle){
				g.DrawRectangle(new Pen(control.ForeColor), 0, 0, control.Width, control.Height);
			}
			if (control is TableLayoutPanel){
				DoPaint(g, (Control) control, addx, addy);
			} else{
				foreach (Control c in control.Controls){
					if (c is BasicControl){
						Debug.WriteLine("- BasicControl");
						g.SetClippingMask(c.Width, c.Height, control.Location.X + c.Location.X + addx,
							control.Location.Y + c.Location.Y + addy);
						((BasicControl) c).view.Print(g, c.Width, c.Height);
					} else{
						DoPaint(g, c);
					}
				}
			}
		}

		internal static void DoPaint(IGraphics g, DataGridView grid){
			Pen p = new Pen(grid.GridColor);
			float x = grid.Location.X;
			float y = grid.Location.Y;
			SolidBrush text = new SolidBrush(grid.ColumnHeadersDefaultCellStyle.ForeColor);
			Font headerFont = grid.ColumnHeadersDefaultCellStyle.Font;
			StringFormat headerformat = new StringFormat{
				Alignment = StringAlignment.Near,
				LineAlignment = StringAlignment.Center
			};
			for (int c = 0; c < grid.Columns.Count; c++){
				SolidBrush header = new SolidBrush(grid.Columns[c].HeaderCell.Style.BackColor);
				if (header.Color.IsEmpty || header.Color == Color.Transparent || header.Color == Color.Black ||
					header.Color.Name == "0"){
					header.Color = grid.ColumnHeadersDefaultCellStyle.BackColor;
				}
				g.FillRectangle(header, x, y, grid.Columns[c].Width, grid.ColumnHeadersHeight);
				g.DrawRectangle(p, x, y, grid.Columns[c].Width, grid.ColumnHeadersHeight);
				g.DrawString(grid.Columns[c].HeaderText, headerFont, text,
					new RectangleF(x, y, grid.Columns[c].Width, grid.ColumnHeadersHeight), headerformat);
				x += grid.Columns[c].Width;
			}
			y += grid.ColumnHeadersHeight;
			for (int r = 0; r < grid.Rows.Count; r++){
				x = grid.Location.X;
				for (int c = 0; c < grid.Columns.Count; c++){
					Color backcolor = GetBackColor(grid, r, c);
					Color forecolor = GetForeColor(grid, r, c);
					if (!backcolor.IsEmpty){
						g.FillRectangle(new SolidBrush(backcolor), x, y, grid.Columns[c].Width, grid.Rows[r].Height);
					}
					g.DrawRectangle(p, x, y, grid.Columns[c].Width, grid.Rows[r].Height);
					object value = grid.Rows[r].Cells[c].Value;
					if (value != null){
						Font font = grid.DefaultCellStyle.Font;
						StringFormat cellformat = GetStringFormat(grid.Columns[c].DefaultCellStyle.Alignment);
						string t = value.ToString();
						if (!string.IsNullOrEmpty(grid.Columns[c].DefaultCellStyle.Format)){
							string format = "{0:" + grid.Columns[c].DefaultCellStyle.Format.ToLower() + "}";
							t = string.Format(format, value);
						}
						g.DrawString(t, font, new SolidBrush(forecolor), new RectangleF(x, y, grid.Columns[c].Width, grid.Rows[r].Height),
							cellformat);
					}
					x += grid.Columns[c].Width;
				}
				y += grid.Rows[r].Height;
			}
		}

		private static void DoPaint(IGraphics g, TextBox textBox){
			g.FillRectangle(new SolidBrush(textBox.BackColor), textBox.Location.X, textBox.Location.Y,
				textBox.Width - textBox.Margin.Left - textBox.Margin.Right,
				textBox.Height - textBox.Margin.Top - textBox.Margin.Bottom);
			RectangleF rect = new RectangleF(textBox.Location, textBox.Size);
			StringFormat format = new StringFormat{Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near};
			g.DrawString(textBox.Text, textBox.Font, new SolidBrush(textBox.ForeColor), rect, format);
		}

		private static void DoPaint(IGraphics g, RichTextBox textBox){
			g.FillRectangle(new SolidBrush(textBox.BackColor), textBox.Location.X, textBox.Location.Y,
				textBox.Width - textBox.Margin.Left - textBox.Margin.Right,
				textBox.Height - textBox.Margin.Top - textBox.Margin.Bottom);
			string[] lines = textBox.Text.Split('\n');
			float h = 0;
			foreach (string t in lines){
				g.DrawString(t, textBox.Font, new SolidBrush(textBox.ForeColor), textBox.Location.X, textBox.Location.Y + h);
				h += textBox.Font.Height*0.8f;
			}
		}

		private static void DoPaint(IGraphics g, Label label){
			RectangleF rect = new RectangleF(label.Location, label.Size);
			g.FillRectangle(new SolidBrush(label.BackColor), rect.X, rect.Y, label.Width - label.Margin.Left - label.Margin.Right,
				label.Height - label.Margin.Top - label.Margin.Bottom);
			StringFormat format = new StringFormat{Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near};
			g.DrawString(label.Text, label.Font, new SolidBrush(label.ForeColor), rect, format);
		}

		private static StringFormat GetStringFormat(DataGridViewContentAlignment alignment){
			StringFormat format = new StringFormat();
			switch (alignment){
				case DataGridViewContentAlignment.BottomCenter:
					format.Alignment = StringAlignment.Center;
					format.LineAlignment = StringAlignment.Far;
					break;
				case DataGridViewContentAlignment.BottomLeft:
					format.Alignment = StringAlignment.Near;
					format.LineAlignment = StringAlignment.Far;
					break;
				case DataGridViewContentAlignment.BottomRight:
					format.Alignment = StringAlignment.Far;
					format.LineAlignment = StringAlignment.Far;
					break;
				case DataGridViewContentAlignment.MiddleCenter:
					format.Alignment = StringAlignment.Center;
					format.LineAlignment = StringAlignment.Center;
					break;
				case DataGridViewContentAlignment.MiddleLeft:
					format.Alignment = StringAlignment.Near;
					format.LineAlignment = StringAlignment.Center;
					break;
				case DataGridViewContentAlignment.MiddleRight:
					format.Alignment = StringAlignment.Far;
					format.LineAlignment = StringAlignment.Center;
					break;
				case DataGridViewContentAlignment.TopCenter:
					format.Alignment = StringAlignment.Center;
					format.LineAlignment = StringAlignment.Near;
					break;
				case DataGridViewContentAlignment.TopLeft:
					format.Alignment = StringAlignment.Near;
					format.LineAlignment = StringAlignment.Near;
					break;
				case DataGridViewContentAlignment.TopRight:
					format.Alignment = StringAlignment.Far;
					format.LineAlignment = StringAlignment.Near;
					break;
				case DataGridViewContentAlignment.NotSet:
					format.Alignment = StringAlignment.Near;
					format.LineAlignment = StringAlignment.Center;
					break;
			}
			return format;
		}

		private static Color GetBackColor(DataGridView grid, int r, int c){
			if (grid.Rows[r].Cells[c].Selected){
				return grid.Rows[r].Cells[c].InheritedStyle.SelectionBackColor;
			}
			Color backcolor = Color.Empty;
			if (!grid.Rows[r].Cells[c].Style.BackColor.IsEmpty){
				backcolor = grid.Rows[r].Cells[c].Style.BackColor;
			}
			if (!grid.Columns[c].DefaultCellStyle.BackColor.IsEmpty){
				backcolor = grid.Columns[c].DefaultCellStyle.BackColor;
			}
			return backcolor;
		}

		private static Color GetForeColor(DataGridView grid, int r, int c){
			if (grid.Rows[r].Cells[c].Selected){
				return grid.Rows[r].Cells[c].InheritedStyle.SelectionForeColor;
			}
			Color forecolor = Color.Empty;
			if (!grid.Rows[r].Cells[c].Style.ForeColor.IsEmpty){
				forecolor = grid.Rows[r].Cells[c].Style.ForeColor;
			}
			if (!grid.Columns[c].DefaultCellStyle.ForeColor.IsEmpty){
				forecolor = grid.Columns[c].DefaultCellStyle.ForeColor;
			}
			return forecolor;
		}

		public static void DoPaintBackground(IGraphics g, Control control){
			if (g is CGraphics && (!control.BackColor.IsEmpty && control.BackColor != Color.Transparent)){
				g.FillRectangle(new SolidBrush(control.BackColor), control.Location.X, control.Location.Y, control.Width,
					control.Height);
			}
			(control as BasicControl)?.view.Print(g, control.Width, control.Height);
			if (control is Panel && ((Panel) control).BorderStyle == BorderStyle.FixedSingle){
				g.DrawRectangle(new Pen(control.ForeColor), control.Location.X, control.Location.Y, control.Width, control.Height);
			}
		}
	}
}