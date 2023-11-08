using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miscare : MonoBehaviour
{
    //  Accesam obiectul Manager pentru a putea folosi scriptul lui de gravitatie
    public GameObject Manager; 
    
    //  Accesam scriptul de gravitatie pentru a putea folosi unghiul si vectorul de gravitatie
    private Gravitatie _gravitatieScript;
    //  Accesam componenta rigid body pentru a-i aplica forte (saritura)
    private Rigidbody2D _myRigidbody;

    [SerializeField]
    public float FortaSaritura = 300;
    [SerializeField]
    public float VitezaMiscare = 20;

    // Start is called before the first frame update
    void Start()
    {
        _gravitatieScript = Manager.GetComponent<Gravitatie>();
        _myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, _gravitatieScript.UnghiGravitatie - 270);

        // Sensul miscarii orizontale = dreapta (X pozitiv) minus stanga (X negativ)
        float miscareOrizontalaSens = (Input.GetKey(KeyCode.D) ? 1 : 0) - (Input.GetKey(KeyCode.A) ? 1 : 0);

        // Definim un vector pe axa X a jucatorului, in sensul decis mai sus, cu lungimea egala vitezei de miscare
        Vector3 miscareOrizontalaVector = new Vector3(miscareOrizontalaSens, 0, 0) * VitezaMiscare;
        // Ajustam lungimea vectorului la viteza jocului
        miscareOrizontalaVector *= Time.deltaTime;

        // Aplicam vectorul obtinut la translatie
        transform.Translate(miscareOrizontalaVector); 

        // Saritura
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            Vector2 directieSusRelativa = -_gravitatieScript.VectorGravitatie.normalized * FortaSaritura;

            _myRigidbody.AddForce(directieSusRelativa);
        }


        // Debug - resetare pozitie
        if (Input.GetKeyDown(KeyCode.R))
            transform.position = new Vector3(-3.25f, -1.25f, -10f);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
