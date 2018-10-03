using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldMovement : MonoBehaviour
{
    public float tileSize;
    private int gridSize;
    private float duration = 0.5f;
    private bool inprogress = false;
    private bool flag = false;
    public static bool EnemyCanMove = false;
    public static bool IsLevelInstantiated = false;
    float yAxis;
    Vector3 endPoint;
    private Transform EnemyCube;
    Vector3 MovementVector;

    public bool PositiveX { get; set; }
    public bool PositiveZ { get; set; }
    public bool MovementInX { get; set; }
    public bool MovementInZ { get; set; }

    void Start()
    {
        //yAxis = gameObject.transform.position.y;
        var ground = GameObject.FindGameObjectWithTag("ground");
        gridSize = Mathf.RoundToInt(ground.transform.lossyScale.x / tileSize) + 1;
        yAxis = 2.5f;
    }


    void Update()
    {
        if (!IsLevelInstantiated)
        {
            return;
        }
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || (Input.GetMouseButtonUp(0)))
        {
            selectPosition();
        }

        if (flag && !Mathf.Approximately(gameObject.transform.position.magnitude, endPoint.magnitude))
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, endPoint, 1 / (duration * (Vector3.Distance(gameObject.transform.position, endPoint))));
        }
        else if (flag && Mathf.Approximately(gameObject.transform.position.magnitude, endPoint.magnitude))
        {
            inprogress = false;
            flag = false;
        }
        if (EnemyCube != null && !inprogress)
        {
            EnemyMove();
        }

    }
    /// <summary>
    /// tıklanan yere gitmeye calısır. Engellemek için üstte kullanılan diğer fonksiyon kullanılır.
    /// </summary>
    void selectPosition()
    {
        RaycastHit hit;
        //Create a Ray on the tapped / clicked position
        Ray ray;
        #region editorSecimi
        //for unity editor
#if UNITY_EDITOR
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //for touch device
#elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
#endif
        #endregion
        if (Physics.Raycast(ray, out hit) && !inprogress)
        {
            //hareket ile ilgili ayaarlar
            inprogress = true;
            flag = true;
            //bitiş

            if ((hit.point - gameObject.transform.localPosition).x < 0)
            {
                PositiveX = false;
            }
            else
            {
                PositiveX = true;
            }

            if ((hit.point - gameObject.transform.localPosition).z < 0)
            {
                PositiveZ = false;
            }
            else
            {
                PositiveZ = true;
            }


            if (Mathf.Abs((hit.point - gameObject.transform.localPosition).x) < Mathf.Abs((hit.point - gameObject.transform.localPosition).z))
            {
                endPoint.x = gameObject.transform.position.x;
                MovementInZ = true;
                MovementInX = false;
                if (PositiveZ)
                {
                    endPoint.z = tileSize * gridSize;
                }
                else
                {
                    endPoint.z = -tileSize * gridSize;
                }

            }
            else
            {
                MovementInZ = false;
                MovementInX = true;
                endPoint.z = gameObject.transform.position.z;
                if (PositiveX)
                {
                    endPoint.x = tileSize * gridSize;
                }
                else
                {
                    endPoint.x = -tileSize * gridSize;
                }
            }
            endPoint.y = yAxis;
            CalculateEnemyDistance();
        }
    }

    void CalculateEnemyDistance()
    {
        RaycastHit hitEnemy;
        MovementVector = endPoint - gameObject.transform.localPosition; // burada daha sona gelmediği için genel hareket vektorü alınır.
        if (Physics.Raycast(transform.position, MovementVector, out hitEnemy))
        {
            EnemyCube = hitEnemy.transform;
            EnemyCanMove = true; //artık nesne ile carpısma yoluna girdiği için enemy hareket edebilir.
            if (MovementInX)
            {
                if (PositiveX)
                {
                    endPoint = new Vector3(hitEnemy.transform.position.x - tileSize, yAxis, hitEnemy.transform.position.z);
                }
                else
                {
                    endPoint = new Vector3(hitEnemy.transform.position.x + tileSize, yAxis, hitEnemy.transform.position.z);
                }
            }
            if (MovementInZ)
            {
                if (PositiveZ)
                {
                    endPoint = new Vector3(hitEnemy.transform.position.x, yAxis, hitEnemy.transform.position.z - tileSize);
                }
                else
                {
                    endPoint = new Vector3(hitEnemy.transform.position.x, yAxis, hitEnemy.transform.position.z + tileSize);
                }
            }

        }
    }

    void EnemyMove()
    {
        Vector3 EnemyMovementVector = new Vector3(MovementVector.x, MovementVector.y, MovementVector.z);
        if (EnemyCanMove)
        {
            if (MovementInX)
            {
                EnemyMovementVector.z = EnemyCube.transform.position.z;
            }
            if (MovementInZ)
            {
                EnemyMovementVector.x = EnemyCube.transform.position.x;
            }
            EnemyCube.transform.position = Vector3.Lerp(EnemyCube.transform.position, EnemyMovementVector, 1 / (duration * (Vector3.Distance(EnemyCube.transform.position, EnemyMovementVector))));
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ground")
        {
            inprogress = false;
            flag = false;
        }

    }
}
