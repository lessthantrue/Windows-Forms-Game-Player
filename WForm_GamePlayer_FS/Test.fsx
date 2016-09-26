#load "Script.fsx"

open System.Windows.Forms.GamePlayerFS
open System.Drawing
open gameControl
open Draw
open Draw.Shapes

let obj = circle (Fill Brushes.Red) 20.

let g = new QuickGame<double>(initial = 0., tick = ((+) 1.), draw = (fun s -> Transform.translate s s >! obj))
startGame 500 500 10 g
|> Observable.add (fun s -> printfn "%A" s)