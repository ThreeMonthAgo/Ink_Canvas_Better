using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Controls;
using System.Windows.Ink;

namespace Ink_Canvas_Better.Controls.ICBInkCanvas;

public class StrokeHistory
{
    public List<HistoryTerm> History = [];
    public int Index = -1; // -1 => History is empty

    #region Add

    public void Add(HistoryTerm term)
    {
        if (Index >= History.Count - 1)
        {
            History.Add(term);
            Index = History.Count - 1; // Sync forcely
        }
        else
        {
            History.RemoveRange(Index + 1, History.Count - Index - 1);
            History.Add(term);
            Index = History.Count - 1; // Sync forcely
        }
    }

    public void Add(StrokeCollection? addedStk, StrokeCollection? removedStk)
    {
        var term = new HistoryTerm()
        {
            Strokes = new(addedStk, removedStk)
        };
        this.Add(term);
    }

    public void Add(StrokeCollection stks) => this.Add(stks, null);

    public void Add(Stroke stk) => this.Add([stk], null);

    #endregion

    public void Redo(InkCanvas inkCanvas)
    {
        if (Index < 0) return;
        var term = History[Index--];
        if (term.Strokes.Item2 != null && term.Strokes.Item2.Count > 0)
        {
            inkCanvas.Strokes.Add(term.Strokes.Item2);
        }
        if (term.Strokes.Item1 != null && term.Strokes.Item1.Count > 0)
        {
            inkCanvas.Strokes.Remove(term.Strokes.Item1);
        }
    }

    public void Undo(InkCanvas inkCanvas)
    {
        if (Index >= History.Count - 1) return;
        var term = History[++Index];
        if (term.Strokes.Item1 != null) inkCanvas.Strokes.Add(term.Strokes.Item1);
        if (term.Strokes.Item2 != null) inkCanvas.Strokes.Remove(term.Strokes.Item2);
    }

    public void Clear(InkCanvas inkCanvas)
    {
        inkCanvas.Strokes.Clear();
    }

    public void ClearHistroy()
    {
        History.Clear();
        Index = -1;
    }
}

public class HistoryTerm
{
    //           Added             Removed
    public Tuple<StrokeCollection, StrokeCollection> Strokes;
}
