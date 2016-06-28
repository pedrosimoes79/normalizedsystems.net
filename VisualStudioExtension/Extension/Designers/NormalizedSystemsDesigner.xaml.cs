using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NormalizedSystems.Net.Designers
{
    /// <summary>
    /// Interaction logic for NormalizedSystemsDesigner.xaml
    /// </summary>
    public partial class NormalizedSystemsDesigner : UserControl
    {
        private bool _editing = false;

        public NormalizedSystemsDesigner(ViewModel viewModel)
        {
            DataContext = viewModel;

            InitializeComponent();

            // wait until we're initialized to handle events
            viewModel.ViewModelChanged += new EventHandler(ViewModelChanged);

            viewModel.PropertyChanged +=
                (x, y) =>
                {
                    if (!_editing)
                        viewModel.DesignerDirty = true;
                };

            this.dgActionElements.AddingNewItem += (sender, e) => { _editing = true; };
            this.dgConditionElements.AddingNewItem += (sender, e) => { _editing = true; };
            this.dgDataElements.AddingNewItem += (sender, e) => { _editing = true; };
            this.dgEventElements.AddingNewItem += (sender, e) => { _editing = true; };
            this.dgFieldElements.AddingNewItem += (sender, e) => { _editing = true; };
            this.dgRuleElements.AddingNewItem += (sender, e) => { _editing = true; };

            this.dgActionElements.CellEditEnding += (sender, e) => { _editing = false; };
            this.dgConditionElements.CellEditEnding += (sender, e) => { _editing = false; };
            this.dgDataElements.CellEditEnding += (sender, e) => { _editing = false; };
            this.dgEventElements.CellEditEnding += (sender, e) => { _editing = false; };
            this.dgFieldElements.CellEditEnding += (sender, e) => { _editing = false; };
            this.dgRuleElements.CellEditEnding += (sender, e) => { _editing = false; };

            this.LostFocus += (sender, e) => { _editing = false; };

            this.dgActionElements.SelectionChanged += (sender, e) => { itemSelected(sender); };
            this.dgConditionElements.SelectionChanged += (sender, e) => { itemSelected(sender); };
            this.dgDataElements.SelectionChanged += (sender, e) => { itemSelected(sender); };
            this.dgEventElements.SelectionChanged += (sender, e) => { itemSelected(sender); };
            this.dgFieldElements.SelectionChanged += (sender, e) => { itemSelected(sender); };
            this.dgRuleElements.SelectionChanged += (sender, e) => { itemSelected(sender); };

            this.dgActionElements.GotFocus += (sender, e) => { itemSelected(sender); };
            this.dgConditionElements.GotFocus += (sender, e) => { itemSelected(sender); };
            this.dgDataElements.GotFocus += (sender, e) => { itemSelected(sender); };
            this.dgEventElements.GotFocus += (sender, e) => { itemSelected(sender); };
            this.dgFieldElements.GotFocus += (sender, e) => { itemSelected(sender); };
            this.dgRuleElements.GotFocus += (sender, e) => { itemSelected(sender); };

            this.dgActionElements.LoadingRow += (sender, e) => { applyAsterisk(sender, e); };
            this.dgConditionElements.LoadingRow += (sender, e) => { applyAsterisk(sender, e); };
            this.dgDataElements.LoadingRow += (sender, e) => { applyAsterisk(sender, e); };
            this.dgEventElements.LoadingRow += (sender, e) => { applyAsterisk(sender, e); };
            this.dgFieldElements.LoadingRow += (sender, e) => { applyAsterisk(sender, e); };
            this.dgRuleElements.LoadingRow += (sender, e) => { applyAsterisk(sender, e); };

            //this.tabApplication.MouseLeftButtonDown += (sender, e) => { (DataContext as ViewModel).OnSelectChanged((DataContext as ViewModel).Application); };

            //this.dgDataElements.MouseDoubleClick += (sender, e) =>
            //{
            //    if(dgDataElements.SelectedItem != null)
            //        CloseableTabItem.AddTab(this.tabs, string.Format("Data: {0}", ((Definitions.DataElement)dgDataElements.SelectedItem).Name) , new DataElementDesigner() { DataContext = dgDataElements.SelectedItem });
            //};

            //this.dgConditionElements.MouseDoubleClick += (sender, e) =>
            //{
            //    if (dgDataElements.SelectedItem != null)
            //        CloseableTabItem.AddTab(this.tabs, string.Format("Condition: {0}", ((Definitions.ConditionElement)dgConditionElements.SelectedItem).Name), new ConditionElementDesigner() { DataContext = dgConditionElements.SelectedItem });
            //};

            var pivots = new List<CollectionViewSource>();
            var pivot = default(CollectionViewSource);

            pivot = this.FindResource("FieldPivot") as CollectionViewSource;
            pivot.Filter += (sender, e) => { filter<Definitions.FieldElement>(e); };
            pivots.Add(pivot);

            pivot = this.FindResource("DataPivot") as CollectionViewSource;
            pivot.Filter += (sender, e) => { filter<Definitions.DataElement>(e); };
            pivots.Add(pivot);

            pivot = this.FindResource("EventPivot") as CollectionViewSource;
            pivot.Filter += (sender, e) => { filter<Definitions.EventElement>(e); };
            pivots.Add(pivot);

            pivot = this.FindResource("ActionPivot") as CollectionViewSource;
            pivot.Filter += (sender, e) => { filter<Definitions.ActionElement>(e); };
            pivots.Add(pivot);

            pivot = this.FindResource("ConditionPivot") as CollectionViewSource;
            pivot.Filter += (sender, e) => { filter<Definitions.ConditionElement>(e); };
            pivots.Add(pivot);

            pivot = this.FindResource("RulePivot") as CollectionViewSource;
            pivot.Filter += (sender, e) => { filter<Definitions.RuleElement>(e); };
            pivots.Add(pivot);
            
            this.tbFilter.KeyUp += (sender, e) =>
            {
                foreach(var p in pivots)
                {
                    p.View.Refresh();
                }
            };
        }

        internal void DoIdle()
        {
            // only call the view model DoIdle if this control has focus
            // otherwise, we should skip and this will be called again
            // once focus is regained
            var viewModel = DataContext as ViewModel;
            if (viewModel != null) // && this.IsKeyboardFocusWithin)
            {
                viewModel.DoIdle();
            }
        }

        private void ViewModelChanged(object sender, EventArgs e)
        {
            // this gets called when the view model is updated because the Xml Document was updated
            // since we don't get individual PropertyChanged events, just re-set the DataContext
            var viewModel = DataContext as ViewModel;
            DataContext = null; // first, set to null so that we see the change and rebind
            DataContext = viewModel;
            viewModel.PropertyChanged +=
                (x, y) =>
                {
                    if (!_editing)
                        viewModel.DesignerDirty = true;
                };
        }

        private void filter<T>(FilterEventArgs e)
            where T : Definitions.Element
        {
            if (!string.IsNullOrEmpty(this.tbFilter.Text))
            {
                e.Accepted = ((T)e.Item).Name.ToUpperInvariant().Contains(this.tbFilter.Text.ToUpperInvariant());
            }
        }

        private void itemSelected(object sender)
        {
            if (sender != null && DataContext != null && sender is DataGrid)
            {
                if (((DataGrid)sender).SelectedItem != null)
                    (DataContext as ViewModel).OnSelectChanged(((DataGrid)sender).SelectedItem);
            }
        }

        private void applyAsterisk(object sender, DataGridRowEventArgs e)
        {
            if (e.Row.Item == CollectionView.NewItemPlaceholder)
            {
                e.Row.HeaderTemplate = (DataTemplate)this.FindResource("AsteriskTemplate");
                e.Row.UpdateLayout();
            }
        }
    }
}
