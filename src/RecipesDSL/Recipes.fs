module Recipes 

open Ingredients
open References

type Recipe = 
    {
        Name:           string 
        Ingredients:    Ingredient list 
        PostUrl:        string option 
        PostTitle:      string option 
        Effort:         EffortScore
    }
and EffortScore = 
    | Minimal 
    | Low 
    | Medium 
    | High 
    | Extreme 

let orderIngredientNotes (recipe: Recipe) = 
    recipe.Ingredients 
    |> List.collect (fun ing -> ing.Notes)
    |> List.mapi ( fun i note -> (note.Id, i+1) )
    |> Map.ofList
