using UnityEngine;
using System.Collections;

public class ChangeColor : MonoBehaviour
{
    private Color maincol;

    Renderer thisRend; //Renderer of our Cube

    float transitionTime = 5f; // Amount of time it takes to fade between colors

    void Start()

    {

        thisRend = GetComponent<Renderer>(); // grab the renderer component on our cube



        //start our coroutine when the game starts

        StartCoroutine(ColorChange());

        //maincol = thisRend.material.GetColor("_MainColor");
    }

    IEnumerator ColorChange()

    {

        //Infinite loop will ensure our coroutine runs all game

        while (true)

        {
            maincol = thisRend.material.GetColor("_MainColor");
            Color newColor = new Color(Random.value, Random.value, Random.value); // Assign newColor to a random color from our array

            float transitionRate = 0; //Create and set transitionRate to 0. This is necessary for our next while loop to function

            /* 1 is the highest value that the Color.Lerp function uses for

             * transitioning between two colors. This while loop will execute

             * until transitionRate is incremented to 1 or higher

             */

            while (transitionRate < 0.1f)

            {
                //this next line is how we change our material color property. We Lerp between the current color and newColor

                thisRend.material.SetColor("_MainColor", newColor);

                transitionRate += Time.deltaTime / transitionTime; // Increment transitionRate over the length of transitionTime
                

                yield return null; // wait for a frame then loop again

            }

            yield return null; // wait for a frame then loop again

        }

    }

}