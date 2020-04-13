using System;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager2 : MonoBehaviour
{    
    public Dictionary<PlanetSettings.Allegiance, int> scoreDictionary;

    void OnEnable(){
        InitializeScoreDictionary();
        PlanetAllegiance.OnAllegianceSet += AllegianceSet;
        PlanetAllegiance.OnAllegianceChange += AllegianceChanged;
    }
    
    void OnDisable(){
        PlanetAllegiance.OnAllegianceSet -= AllegianceSet;
        PlanetAllegiance.OnAllegianceChange -= AllegianceChanged;
    }
    
    void InitializeScoreDictionary(){
        scoreDictionary = new Dictionary<PlanetSettings.Allegiance, int>();
        foreach(PlanetSettings.Allegiance allegiance in Enum.GetValues(typeof(PlanetSettings.Allegiance)))
            scoreDictionary.Add(allegiance, 0);
    }
    
    void AllegianceSet(PlanetSettings.Allegiance newAllegiance){
        scoreDictionary[newAllegiance]++;
    }

    void AllegianceChanged(PlanetSettings.Allegiance newAllegiance, PlanetSettings.Allegiance oldAllegiance){
        scoreDictionary[oldAllegiance]--;
        scoreDictionary[newAllegiance]++;
        CalculateScore();
    }

    void CalculateScore() {
        var totalParticipants = scoreDictionary.Count;
        var totalLost = 0;
        foreach (var scoreDictionaryEntry in scoreDictionary) {
            if (scoreDictionaryEntry.Value == 0) {
                totalLost++;
            }
        }

        if (totalLost == totalParticipants - 1) {
            foreach (var scoreDictionaryEntry in scoreDictionary) 
                if (scoreDictionaryEntry.Value > 0) 
                    Win(scoreDictionaryEntry.Key);
        }
    }

    void Win(PlanetSettings.Allegiance winningAllegiance) {
        Debug.Log($"{winningAllegiance} wins!");
    }
}
