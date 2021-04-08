using System.Collections.Generic;

namespace ClickUpViewer.ViewModels.Chart
{
    public class Series<T>
    {
        public string Name { get; set; }

        public IEnumerable<T> Data { get; set; }
    }
}