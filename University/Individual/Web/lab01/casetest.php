<!DOCTYPE html>
<html>
  <head>
    <meta charset='UTF-8'>
    <title>Case Test</title>
  </head>
  <body>
    <?php
      //works
      function hello(){
        echo "Hello world!";
      }
    
      Function helloWorld(){
        echo "za warldo!";
      }
    
      $a = 20;
    
      //works
      ECHO "Hello! <br>";
      echo "Hello! <br>";
    
      //works
      echo var_dump($a);
      echo "<br>";
      echo Var_Dump($a);
      echo "<br>";
    
      //works
      echo hello();
      echo "<br>";
      echo HeLlO();
      echo "<br>";
    
      //works
      echo helloWorld();
      echo "<br>";
      echo HELLOWorld();
      echo "<br>";
    
      //doesn't work
      echo $A; //doesn't work
      echo "<br>";
      echo $a;
      echo "<br>";
    
      //doesn't work
      echo M_PI;
      echo "<br>";
      echo m_pi; //doesn't work
      echo "<br>";
    
    ?>
  </body>
</html>