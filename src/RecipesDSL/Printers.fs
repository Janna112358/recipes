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
            sprintf "%g" quantity.Amount
        sprintf "%s%s%s" approxText quantityText quantity.Unit.toString

    | Range rangedQuantity -> 
        let quantityText = 
            sprintf "%g-%g" rangedQuantity.Lower rangedQuantity.Upper 
        sprintf "%s%s" quantityText rangedQuantity.Unit.toString

type Measure with 
    member this.toString = printMeasure this


let printSimpleIngredient ingredient = 
    let measureText = 
        ingredient.Measure.toString
        |> sprintf "%s "
    let prepText =
        ingredient.Prep 
        |> printOption (sprintf ", %s")
    sprintf "%s%s%s" measureText ingredient.Item prepText

type SimpleIngredient with 
    member this.toString = printSimpleIngredient this


let printIngredient ingredient refOrder =    
    let primaryText = 
        ingredient.PrimaryOption.toString 
    let otherText = 
        ingredient.OtherOptions
        |> List.map printSimpleIngredient 
    let allOptionsText = 
        Seq.ofList (primaryText :: otherText)
        |> String.concat " OR " 
    let refText = 
        ingredient.Notes 
        |> List.map (fun note -> note.ref refOrder)
        |> Seq.ofList 
        |> String.concat ""
    sprintf "%s %s" allOptionsText refText

type Ingredient with 
    member this.toString = printIngredient this

let printRecipe (recipe: Recipe) = 
    printfn "\n%s" recipe.Name

    let notesOrder = orderIngredientNotes recipe
    
    printfn "\n--Ingredients--"
    recipe.Ingredients
    |> List.iter ( fun ing -> printfn "%s" (ing.toString notesOrder) )
    printfn "  ----\n"
    
    printf "\n--Notes--\n"
    recipe.Ingredients
    |> List.collect ( fun ing -> ing.Notes )
    |> List.iter ( fun note -> 
        printfn "%s" (note.display notesOrder) )
    printfn "  ----\n"
