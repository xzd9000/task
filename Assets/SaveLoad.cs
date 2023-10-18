using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Inventory))]
public class SaveLoad : MonoBehaviour
{
    public const string saveFileLocalPath = "/save.file";

    public bool save = true;

    private Player _player;
    private Inventory _inventory;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _inventory = GetComponent<Inventory>();

        FileStream file = File.Open(Application.persistentDataPath + saveFileLocalPath, FileMode.OpenOrCreate);

        BinaryReader readBin = new BinaryReader(file);

        Item[] allItems = Resources.LoadAll<Item>("");

        try
        {
            for (int i = 0, count, id; i < readBin.BaseStream.Length - 1;)
            {
                if (i == 0)
                {
                    _player.SetHPDirectly(readBin.ReadSingle());
                    i += 4;
                }
                else if (i == 4)
                {
                    _player.maxHP = readBin.ReadSingle();
                    i += 4;
                }
                else
                {
                    count = readBin.ReadInt32();
                    id = readBin.ReadInt32();
                    _inventory.AddItem(new InventoryItem(count, Array.Find(allItems, (i) => i.id == id)));
                    i += 8;
                }
            }
        }
        finally
        {
            readBin.Dispose();
            file.Dispose();
        }
    }

    private void OnApplicationQuit()
    {
        if (save)
        {
            Debug.Log("game is quit, saving");
            SaveData();
        }
    }

    [ContextMenu("Force save")]
    private void SaveData()
    {
        FileStream file = File.Open(Application.persistentDataPath + saveFileLocalPath, FileMode.Create);

        BinaryWriter writeBin = new BinaryWriter(file);
        try
        {
            writeBin.Write(_player.hp);
            writeBin.Write(_player.maxHP);
            for (int i = 0; i < _inventory.length; i++)
            {
                writeBin.Write(_inventory[i].count);
                writeBin.Write(_inventory[i].item.id);
            }
        }
        finally
        {
            writeBin.Dispose();
            file.Dispose();
        }
    }
}
