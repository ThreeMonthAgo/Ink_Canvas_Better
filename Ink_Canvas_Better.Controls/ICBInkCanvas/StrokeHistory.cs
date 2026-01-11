using System;
using System.Collections.Generic;
using System.Diagnostics;
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

    public void Add(Stroke stk)
    {
        HistoryTerm term = new(stk);
        Add(term);
    }

    #endregion

    public void Redo()
    {
        if (Index < 0) return;
        var term = History[Index--];
        if (term.MetaData.IsCleared)
        {
            Index = History.Count - 1;
            for (int i = 0; i <= Index; i++)
            {
                History[i].MetaData.IsCleared = false;
                _inkCanvas.Strokes.Add(History[i].Stroke);
            }
        }
        else
        {
            _inkCanvas.Strokes.Remove(term.Stroke);
        }
    }

    public void Undo()
    {
        if (Index >= History.Count - 1) return;
        var term = History[++Index];
        _inkCanvas.Strokes.Add(term.Stroke);
    }

    public void Clear()
    {
        foreach (var term in History)
        {
            term.MetaData.IsCleared = true;
        }
        _inkCanvas.Strokes.Clear();
    }

    public void ClearHistroy()
    {
        History.Clear();
        Index = -1;
    }
}

public class HistoryTerm(Stroke? stk)
{
    public Stroke? Stroke = stk;
    public HistoryTermMetaData MetaData = new();
}

/// <summary>
/// Provides information for a history term.
/// </summary>
public struct HistoryTermMetaData()
{
    public bool IsCleared = false;
}

