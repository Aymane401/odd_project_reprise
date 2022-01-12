using System.Windows.Controls;

namespace App_UI.Controls
{
    /// <summary>
    /// Cette classe permet de naviguer à l'item sélectionné directement.
    /// Ce que le ListView de base ne fait pas à la base.
    /// </summary>
    public class ListViewScroll : ListView
    {
        public ListViewScroll() : base()
        {
            SelectionChanged += new SelectionChangedEventHandler(ListBoxScroll_SelectionChanged);
        }

        private void ListBoxScroll_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ScrollIntoView(SelectedItem);
        }
    }
}
