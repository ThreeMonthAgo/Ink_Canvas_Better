using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Controls;
using System.Windows.Ink;

namespace Ink_Canvas_Better.Controls.ICBInkCanvas;

public class StrokeHistory(InkCanvas inkCanvas)
{
    public List<HistoryTerm> History = [];
    public int Index = -1; // -1 => History is empty
    private readonly InkCanvas _inkCanvas = inkCanvas;

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

    public void Redo()
    {
        if (Index < 0) return;
        var term = History[Index--];
        if (term.Strokes.Item2 != null && term.Strokes.Item2.Count > 0)
        {
            _inkCanvas.Strokes.Add(term.Strokes.Item2);
        }
        if (term.Strokes.Item1 != null && term.Strokes.Item1.Count > 0)
        {
            _inkCanvas.Strokes.Remove(term.Strokes.Item1);
        }
    }

    public void Undo()
    {
        if (Index >= History.Count - 1) return;
        var term = History[++Index];
        if (term.Strokes.Item1 != null) _inkCanvas.Strokes.Add(term.Strokes.Item1);
        if (term.Strokes.Item2 != null) _inkCanvas.Strokes.Remove(term.Strokes.Item2);
    }

    public void Clear()
    {
        _inkCanvas.Strokes.Clear();
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
