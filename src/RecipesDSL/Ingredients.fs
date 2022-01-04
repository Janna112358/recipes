module Ingredients

open Units
open References

type Measure = 
    | Unspecified                   // no quantity specified at all, e.g. "olive oil" or "vanilla extract"
    | SmallAmount of string option  // any small amount, optionall with specified description string e.g. "bit of ..." or "some ..."
    | Quantity of SimpleQuantity    // amount specified with value (or range) and unit. e.g "200g", "100-150mL" or "2" (unit is number in the last example). Can also be approximate e.g. "~2tbsp"
and SimpleQuantity = 
    {
        Amount:         ValueOrRange
        Unit:           Unit 
        IsApproximate:  bool 
    }
and ValueOrRange = 
    | Value of float 
    | Range of float * float



type Ingredient = 
    | SingleOption of SimpleIngredient 
    | MultipleOptions of SimpleIngredient list
and SimpleIngredient = 
    {
        Item:       string
        Measure:    Measure
        IsOptional: bool
        Prep:       string option
        Notes:      Reference list  
    }


let createQuantity (isApprox: bool) (value: float) (unitSymbol: string) = 
    let unit = Unit.create unitSymbol 
    Quantity { 
        Amount = Value value 
        Unit = unit 
        IsApproximate = isApprox
    }

let createRange (isApprox: bool) (lower: float) (upper: float) (unitSymbol: string) = 
    let unit = Unit.create unitSymbol 
    Quantity{
        Amount = Range (lower, upper) 
        Unit = unit 
        IsApproximate = isApprox
    }

