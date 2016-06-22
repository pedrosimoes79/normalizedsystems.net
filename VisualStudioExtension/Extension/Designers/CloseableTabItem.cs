using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NormalizedSystems.Net.Designers
{
    public class CloseableTabItem : TabItem
    {
        public TabControl TabControl { get; private set; }

        public static void AddTab(TabControl tabs, string text, object content, bool activate = true)
        {
            foreach(var i in tabs.Items)
            {
                var t = i as TabItem;

                if (t.Header is CloseableTabHeader)
                {
                    var h = t.Header as CloseableTabHeader;

                    if(h.Text == text)
                    {
                        if (activate) tabs.SelectedItem = t;

                        return;
                    }
                }
            }

            var header = new CloseableTabHeader() { Text = text };

            var tab =
                new CloseableTabItem()
                {
                    TabControl = tabs,
                    Header = header,
                    Content = content,
                };

            header.Click +=
                (sender, e) =>
                {
                    tabs.Items.Remove(tab);
                };

            tabs.Items.Add(tab);

            if (activate)
            {
                tabs.SelectedItem = tab;
            }
        }
    }
}
