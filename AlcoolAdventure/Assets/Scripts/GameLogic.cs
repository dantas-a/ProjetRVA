using UnityEngine;

// Ce script contient les variables régissant l'avancée dans le jeu
public static class GameLogic
{
    public static int acte = 0; //niveau alcool : 0, 1, 2 ou 3 (voire plus)
    public static int interactionBarman = 0; // 0 : chope vide pas prise; 1 : chope vide prise; 2 : tonneau recupéré; 3 : tonneau donné au barman
    public static int interactionFermier = 0; // 0 : pas encore vomit; 1 : vomit; 2 : champi recuperé; 3 : champi redonné au pegu
    public static int interactionPilleur = 0; // 0 : pas parler au pilleur de tombe; 1 : parler au pilleur à l'acte2; 2 : artéfact recupéré; 3 : artéfact rendu au pilleur; 4 : artefact utilisé pour le rituel
    public static int interactionRituel = 0; // 0 : rituel pas découvert; 1 : rituel découvert; 2 : rituel effectué
}
