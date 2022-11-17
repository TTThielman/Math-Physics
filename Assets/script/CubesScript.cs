using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// identify matrix on cube
public static class MatrixExtensions 
{
    public static Quaternion ExtractRotation(this Matrix4x4 matrix)
    {
        Vector3 forward;
        forward.x = matrix.m02;
        forward.y = matrix.m12;  
        forward.z = matrix.m22;  

        Vector3 upwards;
        upwards.x = matrix.m01; 
        upwards.y = matrix.m11;
        upwards.z = matrix.m21;
        
        return Quaternion.LookRotation(forward, upwards);
    }


    public static Vector3 ExtractPosition(this Matrix4x4 matrix)
    {
        Vector3 position;
        position.x = matrix.m03;
        position.y = matrix.m13;
        position.z = matrix.m23;
        return position;
    }

}


public static class TransformExtensions
 {
     public static void FromMatrix(this Transform transform, Matrix4x4 matrix)
     { 
             transform.rotation = matrix.ExtractRotation();
             transform.position = matrix.ExtractPosition();
     }
 }
    

    public class CubesScript : MonoBehaviour


 {
    public Matrix4x4 transformcube;

    public Slider translationX,translationY ,translationZ;

    public Slider rotX, rotY, rotZ;

    public Vector3 translation;

    public Vector3 rotation;


    private void Start()
    {
        transformcube = new Matrix4x4(
    		            new Vector4(1,0,0,0),
    		            new Vector4(0,1,0,0),
    		            new Vector4(0,0,1,0),
    		            new Vector4(0,0,0,1)
    	); 
    }   

    void Update()
    {
        rotation = new Vector3 (rotX.value, rotY.value, rotZ.value);

        var transformcube = TRCubeMatrix  (translation,rotX.value,rotY.value,rotZ.value);

        
        transform.FromMatrix(transformcube);

                                             
        translation = new Vector3 (translationX.value, translationY.value, translationZ.value);
        
    }


    public Matrix4x4 getTranslation(Vector3 translation)
    {
        return new Matrix4x4(new Vector4(1, 0, 0, 0),
                new Vector4(0, 1, 0, 0),
                new Vector4(0, 0, 1, 0),
                new Vector4(translation.x,translation.y,translation.z,1));                         
    }


    public Matrix4x4 getRotationX(float hoek)
    {
       return new Matrix4x4(new Vector4(1, 0, 0, 0), 
        new Vector4(0, Mathf.Cos(hoek), -Mathf.Sin(hoek), 0), 
        new Vector4(0, Mathf.Sin(hoek), Mathf.Cos(hoek), 0),
        new Vector4(0, 0, 0, 1));
    }


    public Matrix4x4 getRotationY(float hoek)
    {
        return new Matrix4x4(new Vector4(Mathf.Cos(hoek), 0, Mathf.Sin(hoek), 0),
        new Vector4(0, 1, 0, 0),
        new Vector4(-Mathf.Sin(hoek), 0, Mathf.Cos(hoek), 0),
        new Vector4(0, 0, 0, 1));
    }


    public Matrix4x4 getRotationZ(float hoek)
    {
        return new Matrix4x4(new Vector4(Mathf.Cos(hoek), -Mathf.Sin(hoek), 0, 0),
        new Vector4(Mathf.Sin(hoek), Mathf.Cos(hoek), 0, 0),
        new Vector4(0, 0, 1, 0),
        new Vector4(0, 0, 0, 1));
    }


    public Matrix4x4 TRCubeMatrix(Vector3 position, float hoekX, float hoekY, float hoekZ) 
    {
        return getTranslation(position) * getRotationX(hoekX) * getRotationY(hoekY) * getRotationZ(hoekZ);
    }
}
 
