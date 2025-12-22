using System.Collections;

namespace Ink_Canvas_Better.Utilities.DataStructures;

/// <summary>
/// A bidirectional associative container that maintains a one-to-one mapping between elements of type TFirst and TSecond. Based on List
/// </summary>
/// <remarks>
/// <para>
/// <b>Key Design Considerations:</b>
/// <list type="bullet">
///   <item>Type safety: TFirst and TSecond must be distinct types to prevent ambiguity in bidirectional lookups.</item>
///   <item>Performance characteristics: All search operations utilize linear scanning with O(n) complexity, making this implementation suitable only for small collections.</item>
///   <item>Recommended usage: Maximum recommended size is 20 elements.</item>
///   <item>Memory efficiency: Stores elements in two parallel List collections, providing compact memory layout at the cost of search performance.</item>
/// </list>
/// </para>
/// </remarks>
public struct BiDictionary<TFirst, TSecond> : IEnumerable<KeyValuePair<TFirst, TSecond>>
{
    public readonly List<TFirst> Firsts = [];
    public readonly List<TSecond> Seconds = [];

    public BiDictionary()
    {
        if (typeof(TFirst) == typeof(TSecond)) throw new InvalidOperationException("The type of TFirst and TSecond must be different");
    }

    public readonly void Add(TFirst first, TSecond second)
    {
        if (ContainsFirst(first))
        {
            throw new InvalidOperationException($"{first} exists in BiDictionary");
        }
        if (ContainsSecond(second))
        {
            throw new InvalidOperationException($"{second} exists in BiDictionary");
        }
        Firsts.Add(first);
        Seconds.Add(second);
    }

    public readonly void RemoveAt(int index)
    {
        Firsts.RemoveAt(index);
        Seconds.RemoveAt(index);
    }

    public readonly void Remove(TFirst first)
    {
        Seconds.Remove(GetSecond(first));
        Firsts.Remove(first);
    }

    public readonly void Remove(TSecond second)
    {
        Firsts.Remove(GetFirst(second));
        Seconds.Remove(second);
    }

    public readonly void Clear()
    {
        Firsts.Clear();
        Seconds.Clear();
    }

    public readonly IEnumerator<KeyValuePair<TFirst, TSecond>> GetEnumerator()
    {
        for (int i = 0; i < Firsts.Count; i++)
        {
            yield return new KeyValuePair<TFirst, TSecond>(Firsts[i], Seconds[i]);
        }
    }

    readonly IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    #region Contain, Get

    public readonly bool ContainsFirst(TFirst first) => Firsts.Contains(first);

    public readonly TFirst? GetFirst(TSecond second) => Firsts[Seconds.IndexOf(second)];

    public readonly TFirst GetFirst(int index) => Firsts[index];

    public readonly TFirst? this[TSecond second] => GetFirst(second);


    public readonly bool ContainsSecond(TSecond second) => Seconds.Contains(second);

    public readonly TSecond? GetSecond(TFirst first) => Seconds[Firsts.IndexOf(first)];

    public readonly TSecond GetSecond(int index) => Seconds[index];

    public readonly TSecond? this[TFirst first] => GetSecond(first);

    #endregion

    public readonly KeyValuePair<TFirst, TSecond> this[int i] => new(GetFirst(i), GetSecond(i));
}
