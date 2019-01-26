using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity {
    void Hit(GameObject target, string colorCode);
    void TakeHit(GameObject caster, string colorCode);

    string GetColor();
}
