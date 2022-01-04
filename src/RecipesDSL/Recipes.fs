module Recipes 

open Ingredients
open References

type Recipe = 
    {
        Ingredients:    Ingredient list 
        Instructions:   Step list
    }
and Step = 
    {
        Header:     string option 
        Text:       string
        Notes:      Reference list
    }

let orderNotes (recipe: Recipe) = 
    let ingredientNotes = 
        recipe.Ingredients 
        |> List.collect ( fun ingredient -> 
            match ingredient with 
            | SingleOption ing -> ing.Notes 
            | MultipleOptions ings -> 
                ings |> List.collect (fun ing -> ing.Notes) 
        )
    let otherNotes = 
        recipe.Instructions 
        |> List.collect ( fun step -> step.Notes )
    let allNotes = ingredientNotes @ otherNotes
    let notesOrder = 
        allNotes 
        |> List.mapi ( fun i note -> (note.Id, i) )
        |> Map.ofList 
    notesOrder, allNotes

