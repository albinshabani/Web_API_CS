<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>Product API Test</title>
    <style>
        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }
        th, td {
            padding: 10px;
            border: 1px solid #ddd;
        }
        th {
            background-color: #f4f4f4;
        }
    </style>
</head>
<body>
    <h1>Product List</h1>

    <?php
    $apiUrl = 'http://localhost:5144/api/products';

    $response = @file_get_contents($apiUrl);

    if ($response === FALSE) {
        echo "<p style='color:red;'>Failed to fetch data from API.</p>";
    } else {
        $products = json_decode($response, true);

        // Optional: Uncomment to see actual data structure
        // echo "<pre>"; print_r($products); echo "</pre>";

        if (is_array($products)) {
            echo "<table>";
            echo "<tr>
                    <th>ID</th>
                    <th>Category</th>
                    <th>Name</th>
                    <th>Description</th>
                    <th>Price</th>
                    <th>Image</th>
                    <th>Available</th>
                    <th>Created At</th>
                  </tr>";

            foreach ($products as $product) {
                echo "<tr>
                        <td>{$product['id']}</td>
                        <td>{$product['category']}</td>
                        <td>{$product['name']}</td>
                        <td>{$product['description']}</td>
                        <td>\${$product['price']}</td>
                        <td><img src='{$product['imageUrl']}' alt='Image' width='50'></td>
                        <td>" . ($product['isAvailable'] ? 'Yes' : 'No') . "</td>
                        <td>{$product['createdAt']}</td>
                      </tr>";
            }

            echo "</table>";
        } else {
            echo "<p style='color:red;'>Invalid response from API.</p>";
        }
    }
    ?>

</body>
</html>
