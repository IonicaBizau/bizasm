BizAsm
======
My first assembler and interpreter applications, following [this tutorial](http://www.icemanind.com/VMCS.pdf).

## Example

```asm
# Output "Hello" in console
HELLO_WORLD:

 LDA #72
 LDX #$A000
 STA ,X

 LDA #101
 LDX #$A002
 STA ,X

 LDA #108
 LDX #$A004
 STA ,X

 LDA #108
 LDX #$A006
 STA ,X

 LDA #111
 LDX #$A008
 STA ,X

 END HELLO_WORLD
```

## Assembler
Converts the assembly code in byte-code.

```sh
$ ./Assembler/Assembler/bin/Debug/Assembler.exe input-file.asm output-file.biz
```

## Interpreter
Interprets the byte-code and shows the result in the console.

```sh
./BizMachineGUI/BizMachineGUI/bin/Debug/BizMachineGUI.exe output-file.biz
```

## License
The code is licensed under [the MIT License](/LICENSE).
