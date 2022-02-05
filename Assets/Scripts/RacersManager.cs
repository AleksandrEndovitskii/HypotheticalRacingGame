using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RacersManager : MonoBehaviour
{
    // Consider a hypothetical racing game where hundreds of cars race on a field.
    // The updateRacers method shown below updates the cars and eliminates the ones that collide.
    //    Rewrite the method to improve its readability and performance without changing its behaviour.
    //    Describe further changes you would make if you could change its behaviour.
    //    Discuss your reasoning for making these changes.
    private void UpdateRacers(float deltaTimeS, List<Racer> racers)
    {
        // Updates the racers that are alive
        var aliveRacers = racers.Where(racer => racer.IsAlive);
        foreach (var aliveRacer in aliveRacers)
        {
            //Racer update takes milliseconds
            aliveRacer.Update(deltaTimeS * 1000.0f);
        }

        var racersNeedingRemoved = new List<Racer>();
        // Collides
        for (var racerIndex1 = 0; racerIndex1 < racers.Count; racerIndex1++)
        {
            for (var racerIndex2 = 0; racerIndex2 < racers.Count; racerIndex2++)
            {
                var racer1 = racers[racerIndex1];
                var racer2 = racers[racerIndex2];
                if (racerIndex1 != racerIndex2)
                {
                    if (racer1.IsCollidable && racer2.IsCollidable && racer1.IsCollidesWith(racer2))
                    {
                        OnRacerExplodes(racer1);
                        racersNeedingRemoved.Add(racer1);
                        racersNeedingRemoved.Add(racer2);
                    }
                }
            }
        }

        // Gets the racers that are still alive
        var newRacerList = racers.Where(r =>
            !racersNeedingRemoved.Contains(r)).ToList();

        // Get rid of all the exploded racers
        for (var racerIndex = 0; racerIndex != racersNeedingRemoved.Count; racerIndex++)
        {
            var foundRacerIndex = racers.IndexOf(racersNeedingRemoved[racerIndex]);
            if (foundRacerIndex >= 0) // Check we've not removed this already!
            {
                racersNeedingRemoved[racerIndex].Destroy();
                racers.Remove(racersNeedingRemoved[racerIndex]);
            }
        }

        // Builds the list of remaining racers
        racers.Clear();
        for (var racerIndex = 0; racerIndex < newRacerList.Count; racerIndex++)
        {
            racers.Add(newRacerList[racerIndex]);
        }

        for (var racerIndex = 0; racerIndex < newRacerList.Count; racerIndex++)
        {
            newRacerList.RemoveAt(0);
        }
    }

    private void OnRacerExplodes(Racer racer)
    {
        throw new System.NotImplementedException();
    }
}