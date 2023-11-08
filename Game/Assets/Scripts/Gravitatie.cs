using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravitatie : MonoBehaviour
{
    // Accesam obiectul de directie a gravitatiei pentru a-i schimba vizual rotatia (sageata elementului UI din dreapta-jos)
    public GameObject DirectieObj;

    // UnghiGravitatie este in grade
    public float UnghiGravitatie;
    public Vector2 VectorGravitatie;

    public float PutereGravitatie = 9.81f;


    [SerializeField]
    private float _vitezaRotatieGravitatie = 40.0f; 



    // Start is called before the first frame update
    void Start()
    {
        UnghiGravitatie = 270;
        ModificaGravitatie(true);
    }

    // Update is called once per frame
    void Update()
    {
        ModificaGravitatie(false);

    }

    // Daca argumentul "actualizeazaGravitatia" este true, se va executa intreaga metoda (folosit la start pt. initializare)
    void ModificaGravitatie(bool actualizeazaGravitatia)
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            UnghiGravitatie += _vitezaRotatieGravitatie * Time.deltaTime;
            actualizeazaGravitatia = true;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            UnghiGravitatie -= _vitezaRotatieGravitatie * Time.deltaTime;
            actualizeazaGravitatia = true;
        }

        // Daca nu este apasata o sageata, sau functia nu este rulata la Start(), nu e necesara executia functiei in continuare
        // (deoarece nu s-a modificat valoarea unghiului de gravitatie, nu este necesara actualizarea variabilelor dependente).
        if (!actualizeazaGravitatia)
            return;

        UnghiGravitatie = UnghiGravitatie % 360;


        // Definirea vectorului pentru gravitatie in functie de unghi
        float unghiGravitatieRad = Mathf.Deg2Rad * UnghiGravitatie;
        VectorGravitatie.x = Mathf.Cos(unghiGravitatieRad);
        VectorGravitatie.y = Mathf.Sin(unghiGravitatieRad);

        Physics2D.gravity = VectorGravitatie.normalized * PutereGravitatie;


        // Rotirea sagetii din UI pentru a indica directia gravitatiei
        DirectieObj.transform.rotation = Quaternion.Euler(0, 0, UnghiGravitatie - 270);
    }
}
