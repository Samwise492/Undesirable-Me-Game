using System;
using UnityEngine;

[Serializable]
public class VisibleDictionary<K, V>
{
	public VisibleKeyValuePair<K, V>[] Kvps => kvps;

    [SerializeField]
	private VisibleKeyValuePair<K, V>[] kvps;
}

[Serializable]
public class VisibleKeyValuePair<K, V>
{
	public K key;

    public V value;
}