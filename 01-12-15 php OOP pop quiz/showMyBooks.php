<?php
	include("book.php");
?>

<html>
<head>
	<title>MyBooks</title>
</head>
<body>
	<div id="main">
		<?php
			// Create a collection of books
			$book1 = new book("Zombie Survival Guide", 22.99);
			$book2 = new book("Salt", 13.99);
			$book3 = new book("A Most Dangerous Game", 25.49);
			$book4 = new book("Farside Complete Collection", 189.99);
			$book5 = new book("100 Crosswords", 5.99);
			$books = array($book1, $book2, $book3, $book4, $book5);

			// Output the book information
			for ($i = 0; $i < count($books); $i++) {
				$book = $books[$i];
				echo $i + 1 . ": " . $book->GetName() . " ($" . $book->GetPrice() . ")<br>";
			}

		?>
	</div>
</body>
</html>