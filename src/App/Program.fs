open System

open Ingredients
open References
open Recipes
open Printers

let lentils = 
    let quantity = createQuantity true 200.0 "g"
    let primaryOption = 
        {
            Item    = "red lentils (dried)"
            Measure = quantity 
            Prep    = None 
        }
    {
        PrimaryOption   = primaryOption
        OtherOptions    = []
        IsOptional      = false 
        Notes           = []
    }

let onions = 
    let noteAboutOnions = Reference.create "Preferably white/brown onion, whatever you call that"
    let quantity = createRange 1. 2. "#" 
    let primaryOption = 
        {
            Item = "onions"
            Measure = quantity 
            Prep = None 
        }
    {
        PrimaryOption   = primaryOption
        OtherOptions    = [] 
        IsOptional      = false 
        Notes           = [noteAboutOnions]
    }

let noteAboutButter = Reference.create "Like Flora or Violife Block, or a vegan margerine"

let optionalFat = 
    let primaryOption = 
        {
            Item    = "vegan butter"
            Measure = SmallAmount (Some "a bit of")
            Prep    = None
        }
    let otherOptions = [
        {
            Item    = "coconut oil"
            Measure = SmallAmount None 
            Prep    = None 
        }
    ]
    { 
        PrimaryOption   = primaryOption
        OtherOptions    = otherOptions 
        IsOptional      = true 
        Notes           = [noteAboutButter] 
    }

let misirWat: Recipe = 
    {
        Name = "Misir Wat"
        Ingredients = [lentils; onions; optionalFat]
        PostUrl = None
        PostTitle = None 
        Effort = Medium
    }

[<EntryPoint>]
let main args =
    printRecipe misirWat
    0 // return an integer exit code
