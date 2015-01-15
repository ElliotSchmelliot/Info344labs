<?php
	class Book
	{
		private $name;
		private $price;

		public function Book($tName, $tPrice) {
			$this->name = $tName;
			$this->price = $tPrice;
		}

		public function GetName()
		{
			return $this->name;
		}

		public function GetPrice()
		{
			return $this->price;
		}
	}
?>