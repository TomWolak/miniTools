# CheckboxZustandsabhaengigerKlick

Ranorex UserCode helper for intelligent checkbox interaction based on the current checkbox state.

This utility prevents unnecessary clicks by first checking the current checkbox state via the `aria-checked` attribute and only clicking when a state change is required.

Useful for stable UI automation and avoiding unintended toggle behavior.

## Features

- State-dependent checkbox handling
- Prevents unnecessary clicks
- Uses `aria-checked` for reliable state detection
- Supports dynamic boolean values from Aqua variable tables
- Improves stability of UI automation tests

## Use Case

The tester defines the desired checkbox state using a boolean value (`true` or `false`) provided from an Aqua variable table.

The UserCode method:
- reads the current checkbox state,
- compares it with the expected target state,
- performs a click only if necessary.

## Example Logic

| Current State | Expected State | Action |
|---|---|---|
| unchecked | `true` | click |
| checked | `false` | click |
| checked | `true` | no action |
| unchecked | `false` | no action |

## Usage

### 1. Create a repository item for the checkbox

Example repository item name:

```text
Beleg_drucken_Checkbox_zustandsabhaengiger_Klick
```

Example XPath:

```xpath
.//?[@innertext=' ... ']//div[@class~'hux-ui-checkbox']//div[@class~'hux-ui-checkitem hux-ui-check-item']
```

---

### 2. Call the UserCode method

```csharp
SetCheckbox(true);
```

or

```csharp
SetCheckbox(false);
```

---

### 3. Provide the boolean value from Aqua

The value for `shouldBeChecked` is typically provided by the tester via an Aqua variable table connected to the test step.

## Technical Details

The helper evaluates:

```text
aria-checked
```

to determine the current checkbox state.

Only when:

```text
current state != expected state
```

a click action is executed.

This minimizes flaky checkbox interactions and improves test reliability.

## Notes

- Designed for modern UI frameworks using `aria-checked`
- Intended for Ranorex UserCode
- Especially useful for dynamic forms and configurable UI states
- Reduces unstable toggle behavior during repeated test execution