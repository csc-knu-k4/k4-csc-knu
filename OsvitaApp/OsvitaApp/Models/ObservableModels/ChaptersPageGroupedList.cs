using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsvitaApp.Models.ObservableModels
{
    public class ChaptersPageGroupedList : List<ChaptersPageGroupedItem>
    {
        public ChapterObservableModel Chapter { get; set; }

        public ChaptersPageGroupedList(ChapterObservableModel chapter, List<ChaptersPageGroupedItem> topics) : base(topics)
        {

        }
    }

    public class ChaptersPageGroupedItem
    {
        public TopicObservableModel Topic { get; set; }
    }
}
