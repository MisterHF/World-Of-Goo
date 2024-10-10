using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Goo : MonoBehaviour
{
    private Transform _transform;

    private float _minDist = 1.0f;
    private float _maxDist = 10f;
    private bool _isPossed = false;

    private List<Collider2D> _findCollider = new List<Collider2D>();


    // j'ai besoin de r�cup�rer les liens proches du goo et de faire appel � la liste
    // j'ai besoin d'un bool�en pour savoir si le goo et pose ou non 
    // la vitesse des goo qui ne sont pas utilis� et qui ce d�placent dans la construction deja cr��e  //[SerializeField] private float speed = 1.0f;
    // r�cup�rer le transform du monstre pour lui donner le circle Overlap
    // au start r�cup�rer le transform pour evit� de le cr�er en boucle dans le update avec une variable et au start
    // cr�er deux circle overlap pour la distance min et maxi pour poser un goo
    // Creer une liste des colliders rencontre




    // Objectif. poser un goo que j'ai saisi proche de 2 goo d�j� plac� et cr�er des spring joint pour les relier.
    // cr�er 2 circle overlap pour 1: d�tecter la distance minimum avec notre goo 2: la distance max avec les goo
    // garder l'info des colliders des goo dans une liste et conserver les 2 plus proches de celui saisi.
    // une fois les deux plus proches identifi�s. Cr�er 2 spring joint pour se raccorder � ces deux goo
    // et passer l'�tat du goo saisi en goo "pos�"




    void Start()
    {
        _transform = transform;
        
    }

    
    void Update()
    {
       _findCollider = Physics2D.OverlapCircleAll(_transform.position, _maxDist, 1 << LayerMask.NameToLayer("Cell")).ToList();           //utiliser la distance max pour savoir si on peut poser le goo
    }

    private void CreateLink()
    {
        if (_isPossed)
        {
            gameObject.AddComponent<SpringJoint>();
            //gameObject.GetComponent<SpringJoint>().connectedBody = le rigidebody de la cible a relier
        }
    }

    private void FindRigidebody() 
    { 

    }

    private void SortList(List<Collider2D> list)
    {
        //r�cup�rer tous les colliders / r�cup�rer l'infos de chacun des transform de chaque collider / prendre ceux les plus proches de mon transform de "cette cellule / ce" gameobject
    }
}
