using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BaseLib.Forms.Select;
using BaseLibS.Num;
using BaseLibS.Util;

namespace BaseLib.Wpf{
	/// <summary>
	/// Interaction logic for ListSelectorControl.xaml
	/// </summary>
	public partial class ListSelectorControl{
		public event EventHandler SelectionChanged;
		private Thread downThread;
		private Thread upThread;
		private bool repeats;

		public ListSelectorControl(){
			InitializeComponent();
			DownButton.MouseDown += DownButtonMouseDown;
			DownButton.MouseUp += DownButtonMouseUp;
			UpButton.MouseDown += UpButtonMouseDown;
			UpButton.MouseUp += UpButtonMouseUp;
			HasMoveButtons = false;
		}

		public bool Repeats{
			get { return repeats; }
			set { repeats = value; }
		}

		private void DownButtonMouseUp(object sender, MouseEventArgs e){
			downThread.Abort();
			downThread = null;
		}

		private void UpButtonMouseUp(object sender, MouseEventArgs e){
			upThread.Abort();
			upThread = null;
		}

		private void DownButtonMouseDown(object sender, MouseEventArgs e){
			downThread = new Thread(WalkDown);
			downThread.Start();
		}

		private void UpButtonMouseDown(object sender, MouseEventArgs e){
			upThread = new Thread(WalkUp);
			upThread.Start();
		}

		private void WalkDown(){
			Thread.Sleep(400);
			while (true){
				Dispatcher.Invoke(new Action(() => DownButtonClick(null, null)));
				Thread.Sleep(150);
			}
// ReSharper disable FunctionNeverReturns
		}

// ReSharper restore FunctionNeverReturns
		private void WalkUp(){
			Thread.Sleep(400);
			while (true){
				Dispatcher.Invoke(new Action(() => UpButtonClick(null, null)));
				Thread.Sleep(150);
			}
// ReSharper disable FunctionNeverReturns
		}

// ReSharper restore FunctionNeverReturns
		public ItemCollection Items => AllListBox.Items;
		public ItemCollection SelectedItems => SelectedListBox.Items;

		public string[] SelectedStrings{
			get{
				ItemCollection sel = SelectedItems;
				string[] result = new string[sel.Count];
				for (int i = 0; i < result.Length; i++){
					result[i] = sel[i].ToString();
				}
				return result;
			}
		}

		public int[] SelectedIndices{
			get{
				ItemCollection selItems = SelectedItems;
				int[] result = new int[selItems.Count];
				for (int i = 0; i < result.Length; i++){
					result[i] = GetIndexOf(selItems[i]);
				}
				return result;
			}
			set{
				SelectedListBox.Items.Clear();
				foreach (int i in value){
					SetSelected(i, true);
				}
				SelectionChanged?.Invoke(this, new EventArgs());
			}
		}

		private int GetIndexOf(object o){
			ItemCollection items = Items;
			for (int i = 0; i < items.Count; i++){
				if (items[i].Equals(o)){
					return i;
				}
			}
			if (o == null){
				throw new Exception("Object is null.");
			}
			string[] x = new string[items.Count];
			for (int i = 0; i < x.Length; i++){
				x[i] = items[i].ToString();
			}
			throw new Exception(o + " is not contained. Values are " + StringUtils.Concat(",", x));
		}

		public void SetSelected(int index, bool value){
			if (index >= AllListBox.Items.Count || index < 0){
				return;
			}
			object o = AllListBox.Items[index];
			if (value){
				if (!SelectedListBox.Items.Contains(o)){
					SelectedListBox.Items.Add(o);
				}
			} else{
				if (SelectedListBox.Items.Contains(o)){
					SelectedListBox.Items.Remove(o);
				}
			}
		}

		public void SetSelected(object item, bool value){
			if (value){
				if (!SelectedListBox.Items.Contains(item)){
					SelectedListBox.Items.Add(item);
				}
			} else{
				if (SelectedListBox.Items.Contains(item)){
					SelectedListBox.Items.Remove(item);
				}
			}
		}

		private static int[] GetSelectedIndices(ListBox box){
			int[] result = new int[box.SelectedItems.Count];
			for (int i = 0; i < result.Length; i++){
				result[i] = box.Items.IndexOf(box.SelectedItems[i]);
			}
			return result;
		}

		private void SetOrder(IList<int> order, IEnumerable<int> selection){
			object[] items = new object[SelectedListBox.Items.Count];
			for (int i = 0; i < items.Length; i++){
				items[i] = SelectedListBox.Items[i];
			}
			SelectedListBox.Items.Clear();
			SelectedListBox.UnselectAll();
			items = ArrayUtils.SubArray(items, order);
			foreach (object item in items){
				SelectedListBox.Items.Add(item);
			}
			foreach (int i in selection){
				SelectedListBox.SelectedItems.Add(items[i]);
			}
		}

		public bool HasMoveButtons{
			get { return TopButton.Visibility == Visibility.Visible; }
			set{
				TopButton.Visibility = value ? Visibility.Visible : Visibility.Hidden;
				UpButton.Visibility = value ? Visibility.Visible : Visibility.Hidden;
				DownButton.Visibility = value ? Visibility.Visible : Visibility.Hidden;
				BottomButton.Visibility = value ? Visibility.Visible : Visibility.Hidden;
			}
		}

		public static void TrySelect(ListSelector selector, IEnumerable<string> strings){
			if (!strings.Any()){
				return;
			}
			int index = -1;
			for (int i = 0; i < selector.Items.Count; i++){
				string x = (string) selector.Items[i];
				if (StringUtils.ContainsAll(x, strings)){
					index = i;
					break;
				}
			}
			if (index != -1){
				selector.SelectedItems.Add(selector.Items[index]);
			}
		}

		public static void SelectAll(ListBox p0){
			p0.SelectAll();
		}

		public void SetDefaultSelectors(List<string> defaultSelectionNames1, List<string[]> defaultSelections1){
			if (defaultSelectionNames1.Count > 0){
				DefaultButtonPanel.Height = 23;
			}
			for (int i = 0; i < defaultSelectionNames1.Count; i++){
				Button b = new Button{Content = defaultSelectionNames1[i], Width = 80, Height = 23};
				DefaultButtonPanel.Children.Add(b);
				int i1 = i;
				b.Click += (sender, e) => SelectItems(defaultSelections1[i1]);
			}
		}

		private void SelectItems(IEnumerable<string> defaultSelection){
			SelectedListBox.Items.Clear();
			foreach (string s in defaultSelection){
				SelectedListBox.Items.Add(s);
			}
		}

		public void Connect(int connectionId, object target){}

		private void Select_OnClick(object sender, RoutedEventArgs e){
			foreach (object o in AllListBox.SelectedItems){
				if (!SelectedListBox.Items.Contains(o) || repeats){
					SelectedListBox.Items.Add(o);
				}
			}
			SelectionChanged?.Invoke(this, e);
		}

		private void Deselect_OnClick(object sender, RoutedEventArgs e){
			object[] os = new object[SelectedListBox.SelectedItems.Count];
			SelectedListBox.SelectedItems.CopyTo(os, 0);
			foreach (object o in os){
				SelectedListBox.Items.Remove(o);
			}
			SelectionChanged?.Invoke(this, e);
		}

		private void TopButtonClick(object sender, RoutedEventArgs e){
			int[] selectedIndices = GetSelectedIndices(SelectedListBox);
			if (selectedIndices.Length == 0){
				return;
			}
			int n = SelectedListBox.Items.Count;
			int[] unselectedIndices = ArrayUtils.Complement(selectedIndices, n);
			int[] order = ArrayUtils.Concat(selectedIndices, unselectedIndices);
			int[] selection = ArrayUtils.ConsecutiveInts(selectedIndices.Length);
			SetOrder(order, selection);
		}

		private void UpButtonClick(object sender, RoutedEventArgs e){
			int[] selectedIndices = GetSelectedIndices(SelectedListBox);
			if (selectedIndices.Length == 0){
				return;
			}
			Array.Sort(selectedIndices);
			int index = -1;
			for (int i = selectedIndices.Length - 1; i >= 0; i--){
				if (i == 0 || selectedIndices[i - 1] < selectedIndices[i] - 1){
					index = i;
					break;
				}
			}
			int q = selectedIndices[index];
			if (q == 0){
				return;
			}
			int m = selectedIndices.Length - index;
			int n = SelectedListBox.Items.Count;
			int[] order = new int[n];
			for (int i = 0; i < q - 1; i++){
				order[i] = i;
			}
			for (int i = 0; i < m; i++){
				order[q - 1 + i] = q + i;
			}
			order[q + m - 1] = q - 1;
			for (int i = q + m; i < n; i++){
				order[i] = i;
			}
			int[] selection = selectedIndices;
			for (int i = index; i < selection.Length; i++){
				selection[i]--;
			}
			SetOrder(order, selection);
		}

		private void DownButtonClick(object sender, RoutedEventArgs e){
			int[] selectedIndices = GetSelectedIndices(SelectedListBox);
			if (selectedIndices.Length == 0){
				return;
			}
			Array.Sort(selectedIndices);
			int index = -1;
			for (int i = 0; i < selectedIndices.Length; i++){
				if (i == selectedIndices.Length - 1 || selectedIndices[i + 1] > selectedIndices[i] + 1){
					index = i;
					break;
				}
			}
			int q = selectedIndices[0];
			int n = SelectedListBox.Items.Count;
			if (selectedIndices[index] == n - 1){
				return;
			}
			int m = index + 1;
			int[] order = new int[n];
			for (int i = 0; i < q; i++){
				order[i] = i;
			}
			order[q] = q + m;
			for (int i = 0; i < m; i++){
				order[q + 1 + i] = q + i;
			}
			for (int i = q + m + 1; i < n; i++){
				order[i] = i;
			}
			int[] selection = selectedIndices;
			for (int i = 0; i <= index; i++){
				selection[i]++;
			}
			SetOrder(order, selection);
		}

		private void BottomButtonClick(object sender, RoutedEventArgs e){
			int[] selectedIndices = GetSelectedIndices(SelectedListBox);
			if (selectedIndices.Length == 0){
				return;
			}
			int n = SelectedListBox.Items.Count;
			int[] unselectedIndices = ArrayUtils.Complement(selectedIndices, n);
			int[] order = ArrayUtils.Concat(unselectedIndices, selectedIndices);
			int[] selection = ArrayUtils.ConsecutiveInts(n - selectedIndices.Length, n);
			SetOrder(order, selection);
		}

		//protected override void OnKeyDown(KeyEventArgs e){
		//	if (e.Key == Key.X && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control){
		//		//var p = System.Windows.Forms.Control.MousePosition;
		//		//var p = Mouse.GetPosition(Application.Current.MainWindow);
		//		var p = Mouse.GetPosition(this);
		//		IInputElement c = InputHitTest(new Point(p.X, p.Y));
		//		if (c != null){
		//			if (c.Equals(AllListBox)){
		//				SelectAll(AllListBox);
		//			} else if (c.Equals(SelectedListBox)){
		//				SelectAll(SelectedListBox);
		//			}
		//		}
		//	}
		//	base.OnKeyDown(e);
		//}
		private void ListSelectorControl_OnLoaded(object sender, RoutedEventArgs e){
			//KeyDown += (o, e1) =>{
			//};
		}

		private void CommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e){
			SelectedListBox.SelectAll();
		}
	}
}