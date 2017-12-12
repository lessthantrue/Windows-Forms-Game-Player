#load "C:\Users\Nick\Documents\Programming\Projects\WForm_GamePlayer\WForm_GamePlayer_FS\Script.fsx"

open System.Windows.Forms.GamePlayerFS
open System.Windows.Forms
open System.Drawing
open gameControl
open Draw
open Draw.Shapes

let kobj = circle (Fill Brushes.Red) 20.
let mobj color = circle (Fill color) 20.

type mouseState = {loc : Point; state : Brush}
type keyState = {loc : Point; vel : Size}

type gameState = {key : keyState; mouse:mouseState}

let state_init = {key = {loc = new Point(250, 250); vel = new Size(0, 0)}; mouse = {loc = new Point(250, 250); state = Brushes.Red}}

let tick (state:gameState) : gameState = 
    {state with key = {state.key with loc = state.key.loc + state.key.vel}}

let draw (state:gameState) : Draw.Image.Type = 
    Draw.overlayOffset (double state.key.loc.X) (double state.key.loc.Y) kobj Image.empty 
    |> Draw.overlayOffset (double state.mouse.loc.X) (double state.mouse.loc.Y) (mobj state.mouse.state)

let mouse (args:MouseEventArgs)(state:gameState) = 
    let colorSwitch = function
        | MouseButtons.Left -> Brushes.Yellow
        | MouseButtons.Right -> Brushes.Blue
        | _ -> state.mouse.state
    {state with mouse = {loc = args.Location; state = colorSwitch args.Button}}

let key (args:KeyEventArgs)(state:gameState) = 
    let velSwitch = function
        | Keys.Up -> Size(0, -5)
        | Keys.Down -> Size(0, 5)
        | Keys.Left -> Size(-5, 0)
        | Keys.Right -> Size(5, 0)
        | _ -> state.key.vel
    {state with key = {state.key with vel = velSwitch args.KeyCode}}

let g = new QuickGame<gameState>(initial = state_init, tick = tick, draw = draw, mouse = mouse, key = key)
startGame 500 500 10 g
|> Observable.add (fun s -> printfn "%A" s)