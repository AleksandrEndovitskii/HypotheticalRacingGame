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
            aliveRacer.Update(deltaTimeS * RACER_UPDATING_MULTIPLIER); // TODO: make this operation async?
        }

        var explodedRacers = new List<Racer>();
        // Collides
        var collidableRacers = racers.Where(r => r.IsCollidable).ToList();
        for (var i = 0; i < collidableRacers.Count; i++)
        {
            for (var j = i; j < collidableRacers.Count; j++)
            {
                if (i == j)
                {
                    continue;
                }
                var collidableRacer1 = collidableRacers[i];
                var collidableRacer2 = collidableRacers[j];
                if (!collidableRacer1.IsCollidesWith(collidableRacer2) &&
                    !collidableRacer2.IsCollidesWith(collidableRacer1))
                {
                    continue;
                }
                OnRacerExplodes(collidableRacer1);
                OnRacerExplodes(collidableRacer2);
                explodedRacers.Add(collidableRacer1);
                explodedRacers.Add(collidableRacer2);
            }
        }

        // Get rid of all the exploded racers
        foreach (var explodedRacer in explodedRacers)
        {
            explodedRacer.Destroy();
        }
        racers.RemoveAll(r=> explodedRacers.Contains(r));

        // Gets the racers that are still alive
        var notExplodedRacers = racers.Where(r =>
            !explodedRacers.Contains(r)).ToList();

        // Builds the list of remaining racers
        racers = notExplodedRacers; // TODO: remove this?

        notExplodedRacers.Clear(); // TODO: remove this? - have no sense to clear local variable
    }

    private void OnRacerExplodes(Racer racer)
    {
        throw new System.NotImplementedException();
    }
}