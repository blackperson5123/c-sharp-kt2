// Topic: Indexers — T1.2 KeyValueStore (overload by parameter type)
// Задача: реализовать класс со СВЯЗАННЫМИ индексаторами по int и string.
// Требования:
// - Индексатор this[int id] и this[string key] возвращают/устанавливают string значение.
// - get: если ключ/ид неизвестен — бросать KeyNotFoundException.
// - set: если ключ/ид отсутствует — добавить; если есть — заменить значение.
// - null-ключи/строки: при попытке доступа по null string — бросать ArgumentNullException.
// Примечание: цель — понять перегрузку индексаторов по разным типам параметров.

namespace App.Topics.Indexers.T1_2_KeyValueStore;

public class KeyValueStore
{
    private readonly Dictionary<int, string> _byId = new();
    private readonly Dictionary<string, string> _byKey = new(StringComparer.Ordinal);

    public string this[int id]
    {
        get
        {
            if (!_byId.TryGetValue(id, out var value))
            {
                throw new KeyNotFoundException($"ключ не найден: {id}");
            }
            return value;
        }
        set
        {
            _byId[id] = value;
        }
    }

    public string this[string key]
    {
        get
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (!_byKey.TryGetValue(key, out var value))
                throw new KeyNotFoundException($"ключ не найден : '{key}'");
            return value;
        }
        set
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            _byKey[key] = value;
        }
    }
}