using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ControlLibrary
{
    public class ExtendTreeview : System.Windows.Controls.TreeView,IDisposable
    {

        public ExtendTreeview()
            : base()
        {
            this.SelectedItemChanged += ExtendTreeview_SelectedItemChanged;
        }
        public void Dispose()
        {
            this.SelectedItemChanged -= ExtendTreeview_SelectedItemChanged;
        }

        void ExtendTreeview_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            this.SelectedItem = e.NewValue;
        }

        #region SelectedItem Property

        public new object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly new DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(ExtendTreeview), new UIPropertyMetadata(null, OnSelectedItemChanged));

        private static void OnSelectedItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var item = e.NewValue as TreeViewItem;
            ExtendTreeview treeView = sender as ExtendTreeview;
            treeView.SelectTreeViewItem(treeView.ItemsSource == null ? treeView.Items : treeView.ItemsSource, item);
        }

        #endregion

        private TreeViewItem SelectTreeViewItem(IEnumerable Collection, object Value)
        {
            if (Collection == null) return null;
            foreach (TreeViewItem Item in Collection)
            {
                /// Find in current
                if (Value.Equals(Item))
                {
                    Item.IsSelected = true;
                    return Item;
                }

                /// Find in Childs
                if (Item.Items != null)
                {
                    TreeViewItem childItem = this.SelectTreeViewItem(Item.Items, Value);
                    if (childItem != null)
                    {
                        Item.IsExpanded = true;
                        return childItem;
                    }
                }
            }
            return null;
        }
    }
}
