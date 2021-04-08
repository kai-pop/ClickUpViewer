using System.Collections.Generic;

namespace ClickUpViewer.ViewModels.Chart
{
    public class ChartViewModel<T>
    {
        public IEnumerable<string> Categories { get; set; }
        public IEnumerable<Series<T>> Series { get; set; }
    }
}