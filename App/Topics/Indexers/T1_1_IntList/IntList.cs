// Topic: Indexers — T1.1 IntList (basic)
// Задача: реализовать класс динамического списка целых чисел с индексатором this[int index].
// Требования:
// - Свойство Count — текущее количество элементов.
// - Индексатор get должен бросать ArgumentOutOfRangeException при index < 0 или index >= Count.
// - Индексатор set:
//   * если index в диапазоне [0, Count-1] — заменить значение;
//   * если index == Count — добавить значение в конец (расширение на 1);
//   * если index > Count или index < 0 — бросать ArgumentOutOfRangeException.
// Примечание: это упражнение тренирует базовую работу с индексатором.

namespace App.Topics.Indexers.T1_1_IntList;

public class IntList

{
        private List<int> _items;

public IntList()
{
    _items = new List<int>();
}

public int Count => _items.Count;

public int this[int index]
{
    get
    {
        if (index < 0 || index >= Count)
            throw new ArgumentOutOfRangeException(nameof(index));
        return _items[index];
    }
    set
    {
        if (index < 0 || index > Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        if (index == Count)
        {
            _items.Add(value);
        }
        else
        {
            _items[index] = value;
        }
    }
}
    }