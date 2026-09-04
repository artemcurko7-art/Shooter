using System.Collections.Generic;
using System.Linq;

namespace Game.Scripts.Equipment.Replacement
{
    public class ComparisonStat
    {
        public void CompareMain(DisplayStat displayStatDragged, DisplayStat displayStatDropped)
        {
            if (displayStatDragged.Type == displayStatDropped.Type && displayStatDropped.IsPercentageValue == displayStatDragged.IsPercentageValue)
                SetArrowDirection(displayStatDragged, displayStatDropped, displayStatDragged.Value > displayStatDropped.Value, displayStatDropped.Value > displayStatDragged.Value);
            
            if (displayStatDropped.Value == displayStatDragged.Value)
                SetArrowNeutral(displayStatDragged, displayStatDropped);
        }
        
        public void CompareAdditional(List<DisplayStat> displayStatDragged, List<DisplayStat> displayStatDropped)
        {
            var pairedStats = GetSortedDisplayStats(displayStatDragged).Join(
                GetSortedDisplayStats(displayStatDropped),
                from => (from.Type, from.IsPercentageValue),
                to => (to.Type, to.IsPercentageValue),
                (from, to) => (Dragged: from, Dropped: to)
            );
            
            foreach (var pair in pairedStats)
            {
                SetArrowDirection(pair.Dragged, pair.Dropped, pair.Dragged.Value > pair.Dropped.Value, pair.Dropped.Value > pair.Dragged.Value);

                if (pair.Dragged.Value == pair.Dropped.Value)
                    SetArrowNeutral(pair.Dragged, pair.Dropped);
            }
        }
        
        private List<DisplayStat> GetSortedDisplayStats(List<DisplayStat> displayStat)
        {
            return displayStat
                .OrderBy(s => s.Type)
                .ThenByDescending(s => s.IsPercentageValue)
                .ThenByDescending(s => s.Value)
                .ToList();
        }

        private void SetArrowDirection(DisplayStat displayStatDragged, DisplayStat displayStatDropped, bool isArrowDirectionDragged, bool isArrowDirectionDropped)
        {
            displayStatDragged.SetArrowDirection(isArrowDirectionDragged);
            displayStatDropped.SetArrowDirection(isArrowDirectionDropped);
        }
        
        private void SetArrowNeutral(DisplayStat displayStatDragged, DisplayStat displayStatDropped)
        {
            displayStatDragged.SetArrowNeutral();
            displayStatDropped.SetArrowNeutral();
        }
    }
}