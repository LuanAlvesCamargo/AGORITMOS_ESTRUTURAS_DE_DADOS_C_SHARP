using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures.Hashing.NumberTheory;

namespace DataStructures.Hashing;

public class HashTable<TKey, TValue> {
    private const int DefaultCapacity = 16;
    private const float DefaultLoadFactor = 0.75f;
    private readonly float loadFactor;
    private int capacity;
    private int size;
    private int threshold;
    private int version;
    private Entry<TKey, TValue>?[] entries;
    public int Count => size;
    public int Capacity => capacity;
    public float LoadFactor => loadFactor;
    public IEnumerable<TKey> Keys => entries.Where(e => e != null).Select(e => e!.Key!);
    public IEnumerable<TValue> Values => entries.Where(e => e != null).Select(e => e!.Value!);

    public TValue this[TKey? key] {
        get {
            if (EqualityComparer<TKey>.Default.Equals(key, default(TKey))) {
                throw new ArgumentNullException(nameof(key));
            }
            var entry = FindEntry(key);
            if (entry == null) {
                throw new KeyNotFoundException();
            }
            return entry.Value!;
        }

        set {
            if (EqualityComparer<TKey>.Default.Equals(key, default(TKey))) {
                throw new ArgumentNullException(nameof(key));
            }
            var entry = FindEntry(key);
            if (entry == null) {
                throw new KeyNotFoundException();
            }
            entry.Value = value;
            version++;
        }
    }

    public HashTable(int capacity = DefaultCapacity, float loadFactor = DefaultLoadFactor) {
        if (capacity <= 0) {
            throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be greater than 0");
        }

        if (loadFactor <= 0) {
            throw new ArgumentOutOfRangeException(nameof(loadFactor), "Load factor must be greater than 0");
        }

        if (loadFactor > 1) {
            throw new ArgumentOutOfRangeException(nameof(loadFactor), "Load factor must be less than or equal to 1");
        }

        this.capacity = PrimeNumber.NextPrime(capacity);
        this.loadFactor = loadFactor;
        threshold = (int)(this.capacity * loadFactor);
        entries = new Entry<TKey, TValue>[this.capacity];
    }

    public void Add(TKey? key, TValue? value) {
        if (EqualityComparer<TKey>.Default.Equals(key, default(TKey))) {
            throw new ArgumentNullException(nameof(key));
        }

        if (size >= threshold) {
            Resize();
        }

        var index = GetIndex(key);
        if (entries[index] != null && EqualityComparer<TKey>.Default.Equals(entries[index]!.Key!, key)) {
            throw new ArgumentException("Key already exists");
        }

        if (EqualityComparer<TValue>.Default.Equals(value, default(TValue))) {
            throw new ArgumentNullException(nameof(value));
        }

        entries[index] = new Entry<TKey, TValue>(key!, value!);
        size++;
        version++;
    }
    public bool Remove(TKey? key)
    {
        if (EqualityComparer<TKey>.Default.Equals(key, default(TKey))) {
            throw new ArgumentNullException(nameof(key));
        }

        var index = GetIndex(key);
        if (entries[index] == null) {
            return false;
        }

        entries[index] = null;
        size--;
        version++;

        if (size <= threshold / 4) {
            Resize();
        }
        return true;
    }

    public int GetIndex(TKey? key) {
        if (EqualityComparer<TKey>.Default.Equals(key, default(TKey))) {
            throw new ArgumentNullException(nameof(key));
        }

        var hash = key!.GetHashCode();
        var index = hash % capacity;

        if (index < 0) {
            index += capacity;
        }
        return index;
    }
    public Entry<TKey, TValue>? FindEntry(TKey? key) {
        if (EqualityComparer<TKey>.Default.Equals(key, default(TKey))) {
            throw new ArgumentNullException(nameof(key));
        }
        var index = GetIndex(key);
        return entries[index];
    }

    public bool ContainsKey(TKey? key) {
        if (EqualityComparer<TKey>.Default.Equals(key, default(TKey))) {
            throw new ArgumentNullException(nameof(key));
        }
        return FindEntry(key) != null;
    }

    public bool ContainsValue(TValue? value) {
        if (EqualityComparer<TValue>.Default.Equals(value, default(TValue))) {
            throw new ArgumentNullException(nameof(value));
        }
        return entries.Any(e => e != null && e.Value!.Equals(value));
    }

    public void Clear() {
        capacity = DefaultCapacity;
        threshold = (int)(capacity * loadFactor);
        entries = new Entry<TKey, TValue>[capacity];
        size = 0;
        version++;
    }
    public void Resize() {
        var newCapacity = capacity * 2;
        var newEntries = new Entry<TKey, TValue>[newCapacity];

        foreach (var entry in entries) {
            if (entry == null) {
                continue;
            }
            var index = entry.Key!.GetHashCode() % newCapacity;
            if (index < 0) {
                index += newCapacity;
            }
            newEntries[index] = entry;
        }

        capacity = newCapacity;
        threshold = (int)(capacity * loadFactor);
        entries = newEntries;
        version++;
    }
}
