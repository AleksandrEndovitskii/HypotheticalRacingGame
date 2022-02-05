using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RacersManager : MonoBehaviour
{
    private const float RACER_UPDATING_MULTIPLIER = 1000.0f;

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
            aliveRacer.Update(deltaTimeS * RACER_UPDATING_MULTIPLIER);
        }

        var explodedRacers = new List<Racer>();
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
                        explodedRacers.Add(racer1);
                        explodedRacers.Add(racer2);
                    }
                }
            }
        }

        // Gets the racers that are still alive
        var notExplodedRacers = racers.Where(r =>
            !explodedRacers.Contains(r)).ToList();

        // Get rid of all the exploded racers
        for (var racerIndex = 0; racerIndex != explodedRacers.Count; racerIndex++)
        {
            var foundRacerIndex = racers.IndexOf(explodedRacers[racerIndex]);
            if (foundRacerIndex >= 0) // Check we've not removed this already!
            {
                explodedRacers[racerIndex].Destroy();
                racers.Remove(explodedRacers[racerIndex]);
            }
        }

        // Builds the list of remaining racers
        racers = notExplodedRacers;

        notExplodedRacers.Clear();
    }

    private void OnRacerExplodes(Racer racer)
    {
        throw new System.NotImplementedException();
    }
}