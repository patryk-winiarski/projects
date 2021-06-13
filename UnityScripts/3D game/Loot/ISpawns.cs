﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawns
{
    GameObject itemSpawned { get; set; }
    Rigidbody itemRigidbody { get; set; }
    Renderer itemMaterial { get; set; }
    ItemPickUp itemType { get; set; }

    void CreateSpawn();
}
