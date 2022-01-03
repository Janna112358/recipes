module Printers 

open Ingredients

let printOption (printer: 'T -> string) (option: Option<'T>) = 
    match option with 
    | None -> ""
    | Some x -> printer x

let printMeasure measure = 
    let approxText = 
        if measure.IsApproximate then "~" else ""
    let quantityText = 
        match measure.Quantity with 
        // %g for compact number formatting
        | Quantity q -> sprintf "%g" q
        | Range (lower, upper) -> sprintf "%g-%g" lower upper
    sprintf "%s%s%s" approxText quantityText measure.Unit.toString

type Measure with 
    member this.toString = printMeasure this


let printSimpleIngredient ingredient = 
    let measureText = 
        ingredient.Measure 
        |> printOption (fun m -> sprintf "%s " m.toString)
    let prepText =
        ingredient.Prep 
        |> printOption (fun p -> sprintf ", %s" p)
    sprintf "%s%s%s" measureText ingredient.Item prepText

type SimpleIngredient with 
    member this.toString = printSimpleIngredient this


let printIngredient ingredient =    
    match ingredient with 

    | SingleOption ingredient -> 
        ingredient.toString

    | MultipleOptions ingredientList -> 
        ingredientList 
        |> List.map ( fun ingr -> ingr.toString )
        |> List.toSeq
        |> String.concat " OR "

type Ingredient with 
    member this.toString = printIngredient this

                