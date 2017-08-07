<?php
    class Car{
      private $rank;
      private $year;
      private $make;
      private $model;
      
      function __constructor($r, $y, $ma, $mo){
        $this-setRank($r);
        $this->setYear($y);
        $this->setMake($ma);
        $this->setModel($mo);
      }
      
      function getRank(){
        return $this->rank;
      }
      function getYear(){
        return $this->year;
      }
      function getMake(){
        return $this->make;
      }
      function getModel(){
        return $this->model;
      }
      function setRank($newRank){
        $this->rank = $newRank;
      }
      function setYear($newYear){
        $this->year = $newYear;
      }
      function setMake($newMake){
        $this->make = $newMake;
      }
      function setModel($newModel){
        $this->model = $newModel;
      }
    }
?>