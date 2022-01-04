module Printers 

open Ingredients
open Recipes

let printOption (printer: 'T -> string) (option: Option<'T>) = 
    match option with 
    | None -> ""
    | Some x -> printer x

let printMeasure measure = 
    match measure with 
    | Unspecified -> ""

    | SmallAmount textOption -> 
        textOption 
        |> Option.defaultValue "a small amount of"

    | Quantity quantity ->    
        let approxText = 
            if quantity.IsApproximate then "~" else ""
        let quantityText = 
            match quantity.Amount with 
            // %g for compact number formatting
            | Value value -> sprintf "%g" value
            | Range (lower, upper) -> sprintf "%g-%g" lower upper
        sprintf "%s%s%s" approxText quantityText quantity.Unit.toString

type Measure with 
    member this.toString = printMeasure this


let printSimpleIngredient ingredient refOrder = 
    let measureText = 
        ingredient.Measure.toString
        |> sprintf "%s "
    let prepText =
        ingredient.Prep 
        |> printOption (sprintf ", %s")
    let references = 
        ingredient.Notes 
        |> List.map (fun note -> note.ref refOrder)
        |> List.toSeq 
        |> String.concat " "
    sprintf "%s%s%s%s" measureText ingredient.Item prepText references

type SimpleIngredient with 
    member this.toString = printSimpleIngredient this


let printIngredient ingredient refOrder =    
    match ingredient with 

    | SingleOption ingredient -> 
        ingredient.toString refOrder

    | MultipleOptions ingredientList -> 
        ingredientList 
        |> List.map ( fun ingr -> ingr.toString refOrder )
        |> List.toSeq
        |> String.concat " OR "

type Ingredient with 
    member this.toString = printIngredient this


let printRecipe (recipe: Recipe) = 
    let refOrder, notes = orderNotes recipe

    let printStep (step: Step) = 
        let headerText = 
            step.Header 
            |> printOption (sprintf "%s\n")
        let references = 
            step.Notes 
            |> List.map (fun note -> note.ref refOrder)
            |> List.toSeq 
            |> String.concat " "
        sprintf "%s%s%s" headerText step.Text references
    
    printfn "\n--Ingredients--"
    recipe.Ingredients
    |> List.iter ( fun ing -> printfn "%s" (ing.toString refOrder) )
    printfn "  ----\n"
    
    recipe.Instructions
    |> List.iter ( fun step -> printfn "%s" (printStep step) )

    printf "\n--Notes--\n"
    notes 
    |> List.iter ( fun note -> printfn "%s" (note.display refOrder) )
    printfn "  ----\n"
