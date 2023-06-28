INCLUDE Global.ink

{ somethingNothingChoice == "": -> main | -> already_chose }

-> main
=== main ===
This is a test script, what would you like to test?
    + [Something] -> chosen("something")
    + [Nothing] -> chosen("... wait, nothing? then why are doing this ")
== chosen(choice) ==
~ somethingNothingChoice = choice
You chose to test {choice}.
-> END

== already_chose ==
You already chose {somethingNothingChoice}!
-> END
