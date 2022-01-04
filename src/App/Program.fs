open System

open Ingredients
open References
open Recipes
open Printers

let lentils = 
    let quantity = createQuantity true 200.0 "g"
    Ingredients.SingleOption {
        Item = "red lentils (dried)"
        Measure = quantity
        IsOptional  = false
        Prep        = None
        Notes       = []
    }

let noteAboutButter = Reference.create "Like Flora or Violife Block, or a vegan margerine"

let optionalFat = Ingredients.MultipleOptions [
    {
        Item = "vegan butter"
        Measure = SmallAmount (Some "a bit of")
        IsOptional = true 
        Prep = None 
        Notes = [noteAboutButter]
    }
    {
        Item = "coconut oil"
        Measure = SmallAmount None
        IsOptional = true 
        Prep = None 
        Notes = []
    }
]

let misirWat: Recipe = 
    let testNote = Reference.create "This recipe is incomplete"
    let anotherTestNote = Reference.create "Hi"
    {
        Ingredients = [lentils; optionalFat]
        Instructions = [
            {
                Header = Some "Method"
                Text = "Instructions text here"
                Notes = [testNote; anotherTestNote]
            }
        ]
    }

[<EntryPoint>]
let main args =
    printRecipe misirWat
    0 // return an integer exit code
