# Practise

Starting practise of nouns means you will be able to fill in the article (other use cases coming).  So write a 
component that fetches a single noun from the server and displays it in a form.  The form should have a text input for
the noun and a select input for the article.  The select input should have three options: "der", "die", and "das".  

The form does not need a submit button, as the die/der/das selection takes care of that.  

Once the user selects die/der/das:
- the exercise is checked for correctness - any incorrect answers should be highlighted
and the user can move on.
- the users answer attempt (correct/false) is recorded in the DB
- the next example Noun is fetched from the server.

