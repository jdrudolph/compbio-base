using System;
using System.Drawing;

namespace BaseLib.Graphic{
	/// <remarks/>
	[Serializable, System.Diagnostics.DebuggerStepThrough,
	 System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/svg"),
	 System.Xml.Serialization.XmlRoot(ElementName = "svg", Namespace = "http://www.w3.org/2000/svg", IsNullable = false)]
	
	public class Svg{
		public Svg(){
			OnLoad = "Init(evt)";
			Version = "1.1";
			BaseProfile = "full";
			AddToolTip();
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlElement("title")]
		public string Title { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("desc")]
		public string Description { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("script")]
		public Script Script { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("circle")]
		public Circle[] Circle { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("ellipse")]
		public Ellipse[] Ellipse { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("rect")]
		public Rect[] Rect { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("line")]
		public Line[] Line { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("polygon")]
		public Polygon[] Polygon { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("polyline")]
		public Polyline[] Polyline { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("path")]
		public Path[] Path { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("text")]
		public Text[] Text { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("image")]
		public SvgImage[] SvgImage { get; set; }
		///// <remarks/>
		//[System.Xml.Serialization.XmlElement("g")]
		//public Group[] Groups { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("onload")]
		public string OnLoad { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("version")]
		public string Version { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("baseProfile")]
		public string BaseProfile { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("width")]
		public double Width { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("height")]
		public double Height { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("ViewBox")]
		public string ViewBox { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("g")]
		public Tooltip Tooltip { get; set; }

		public void AddToolTip(){
			Tooltip = new Tooltip();
		}
	}

	/// <remarks/>
	[Serializable, System.Diagnostics.DebuggerStepThrough,
	 System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/svg"),
	 System.Xml.Serialization.XmlRoot(ElementName = "g", Namespace = "http://www.w3.org/2000/svg", IsNullable = false)]
	
	public class Group{
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("circle")]
		public Circle[] Circle { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("ellipse")]
		public Ellipse[] Ellipse { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("rect")]
		public Rect[] Rect { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("line")]
		public Line[] Line { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("polygon")]
		public Polygon[] Polygon { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("polyline")]
		public Polyline[] Polyline { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("path")]
		public Path[] Path { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("text")]
		public Text[] Text { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("image")]
		public SvgImage[] SvgImage { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("style")]
		public string Style { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("transform")]
		public string Transform { get; set; }
	}

	public class SvgElement{
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("transform")]
		public string Transform { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("style")]
		public string Style { get; set; }
	}

	#region Basic Elements

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.1432"), Serializable, System.Diagnostics.DebuggerStepThrough,
	 System.ComponentModel.DesignerCategory("code"),
	 System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/svg")]
	
	public class Circle : SvgElement{
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("cx")]
		public float X { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("cy")]
		public float Y { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("r")]
		public float R { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("stroke")]
		public string Stroke { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("stroke-width")]
		public float StrokeWidth { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("fill")]
		public string Fill { get; set; }
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.1432"), Serializable, System.Diagnostics.DebuggerStepThrough,
	 System.ComponentModel.DesignerCategory("code"),
	 System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/svg")]
	
	public class Ellipse : SvgElement{
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("cx")]
		public double Cx { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("cy")]
		public double Cy { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("rx")]
		public double Rx { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("ry")]
		public double Ry { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("fill")]
		public string Fill { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("stroke")]
		public string Stroke { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("stroke-width")]
		public float StrokeWidth { get; set; }
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.1432"), Serializable, System.Diagnostics.DebuggerStepThrough,
	 System.ComponentModel.DesignerCategory("code"),
	 System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/svg")]
	
	public class Rect : SvgElement{
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("id")]
		public string Id { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("x")]
		public double X { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("y")]
		public double Y { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("rx")]
		public double Rx { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("ry")]
		public double Ry { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("width")]
		public double Width { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("height")]
		public double Height { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("fill")]
		public string Fill { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("stroke")]
		public string Stroke { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("stroke-width")]
		public float StrokeWidth { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("title")]
		public string Title { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("desc")]
		public string Description { get; set; }
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.1432"), Serializable, System.Diagnostics.DebuggerStepThrough,
	 System.ComponentModel.DesignerCategory("code"),
	 System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/svg")]
	
	public class Line : SvgElement{
		public Line(){
			Y2 = 0;
			X2 = 0;
			Y1 = 0;
			X1 = 0;
			Stroke = Color.Black.ToString();
			Strokewidth = 1f;
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("x1")]
		public double X1 { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("y1")]
		public double Y1 { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("x2")]
		public double X2 { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("y2")]
		public double Y2 { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("title")]
		public string Title { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("desc")]
		public string Description { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("stroke")]
		public string Stroke { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("stroke-width")]
		public float Strokewidth { get; set; }
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.1432"), Serializable, System.Diagnostics.DebuggerStepThrough,
	 System.ComponentModel.DesignerCategory("code"),
	 System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/svg")]
	
	public class SvgImage : SvgElement{
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("x")]
		public double X { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("y")]
		public double Y { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("width")]
		public double Width { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("height")]
		public double Height { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("href")]
		public string Href { get; set; }
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.1432"), Serializable, System.Diagnostics.DebuggerStepThrough,
	 System.ComponentModel.DesignerCategory("code"),
	 System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/svg")]
	
	public class Polygon : SvgElement{
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("points")]
		public string Points { get; set; }
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.1432"), Serializable, System.Diagnostics.DebuggerStepThrough,
	 System.ComponentModel.DesignerCategory("code"),
	 System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/svg")]
	
	public class Polyline : SvgElement{
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("points")]
		public string Points { get; set; }
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.1432"), Serializable, System.Diagnostics.DebuggerStepThrough,
	 System.ComponentModel.DesignerCategory("code"),
	 System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/svg")]
	
	public class Path : SvgElement{
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("d")]
		public string D { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("stroke")]
		public string Stroke { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("stroke-width")]
		public float StrokeWidth { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("stroke-dasharray")]
		public string StrokeDashArray { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("fill")]
		public string Fill { get; set; }
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.1432"), Serializable, System.Diagnostics.DebuggerStepThrough,
	 System.ComponentModel.DesignerCategory("code"),
	 System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/svg")]
	
	public class Text : SvgElement{
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("id")]
		public string Id { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("x")]
		public double X { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("y")]
		public double Y { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("dx")]
		public string Dx { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("dy")]
		public string Dy { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("text-anchor")]
		public string Anchor { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("writing-mode")]
		public string Mode { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("fill")]
		public string Fill { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("font-family")]
		public string FontFamily { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("font-size")]
		public double FontSize { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("font-weight")]
		public string FontWeight { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("color")]
		public string Color { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("rotate")]
		public string Rotate { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlText]
		public string Value { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("title")]
		public string Title { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("desc")]
		public string Description { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("tspan")]
		public TSpan[] TSpan { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("stroke")]
		public string Stroke { get; set; }
	}

	#endregion

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.1432"), Serializable, System.Diagnostics.DebuggerStepThrough,
	 System.ComponentModel.DesignerCategory("code"),
	 System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/svg")]
	
	public class Tooltip{
		public Tooltip(){
			PointerEvents = "none";
			Visibility = "hidden";
			Opacity = 0.8;
			Id = "ToolTip";
			Rect = new Rect
			{Id = "tipbox", Fill = "white", Stroke = "black", X = 0, Y = 5, Width = 100, Height = 20, Rx = 2, Ry = 2};
			Text = new Text{Id = "tipText", X = 5, Y = 20, FontFamily = "Tahoma", FontSize = 10, TSpan = new TSpan[2]};
			Text.TSpan[0] = new TSpan{Id = "tipTitle", X = 5, Y = 20, Style = "font-weight: bold", Text = "<![CDATA[]]>"};
			Text.TSpan[1] = new TSpan{Id = "tipDesc", X = 5, Y = 20, Dy = 15, Style = "fill: gray", Text = "<![CDATA[]]>"};
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("id")]
		public string Id { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("opacity")]
		public double Opacity { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("visibility")]
		public string Visibility { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("pointer-events")]
		public string PointerEvents { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("rect")]
		public Rect Rect { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("text")]
		public Text Text { get; set; }
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.1432"), Serializable, System.Diagnostics.DebuggerStepThrough,
	 System.ComponentModel.DesignerCategory("code"),
	 System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/svg")]
	
	public class TSpan{
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("id")]
		public string Id { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("x")]
		public double X { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("y")]
		public double Y { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("dx")]
		public double Dx { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("dy")]
		public double Dy { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("style")]
		public string Style { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlText]
		public string Text { get; set; }
	}

	/// <remarks/>
	[Serializable, System.Diagnostics.DebuggerStepThrough, System.ComponentModel.DesignerCategory("code"),
	 System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/svg")]
	
	public class Script{
		public Script(){
			Type = "text/ecmascript";
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("type")]
		public string Type { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute("href")]
		public string Href { get; set; }
		/// <remarks/>
		[System.Xml.Serialization.XmlText]
		public string Text { get; set; }
	}
}