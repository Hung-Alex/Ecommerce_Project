# Oganic-Store

## Introduction
This project is a full-stack web application built using modern web technologies. It features a responsive frontend and a robust backend to handle various functionalities.

## Features
- Responsive design with TailwindCSS
- Component-based architecture with React
- Material UI for pre-styled components
- State management
- PostCSS for processing CSS
- RESTful API backend
  ### Backend
  Product Management:
    Add, delete, and edit products.
    Manage product information such as name, price, description, images, and stock status.
  Category Management:
    Add, delete, and edit product categories.
  Brand Management:
    Add, delete, and edit product brands.
    News Management:
    Add, delete, and edit news and blog posts.
  Slide Management:
    Add, delete, and edit promotional slides on the homepage.
  Wishlist:
    Add products to the wishlist.
    Remove products from the wishlist.
  Banner Management:
    Add, delete, and edit promotional banners on the website.
  User Authentication:
    Login, registration.
    JWT authentication.
    OAuth with Google.
    Dynamic Authorization:
    Manage user access rights based on roles and permissions.
  Order Management:
    View, cancel, and track order status.
  Cart Management:
    Add products to the cart.
    Remove and edit product quantities in the cart.
    Account Suspension: 
    Suspend and reactivate user accounts.
  User Management:
    Manage user information and roles.
  Reviews and Comments:
    Allow users to review and comment on products.
  Payment:
    Integrate payment processing through VNPay.
Frontend
Product Management: Implement UI for adding, deleting, and editing product details, including name, price, description, images, and stock status.
Category Management: Create interfaces for managing product categories.
Brand Management: Design UI for managing product brands.
News Management: Develop components for adding, deleting, and editing news and blog posts.
Slide Management: Build interfaces for managing promotional slides on the homepage.
Wishlist: Implement functionality for adding and removing products from the wishlist.
Banner Management: Create UI for managing promotional banners on the website.
User Authentication: Develop login and registration forms, JWT authentication, and OAuth with Google integration.
Dynamic Authorization: Design user role and permission management features.
Order Management: Build interfaces for viewing, canceling, and tracking order status.
Cart Management: Implement cart functionality for adding, removing, and editing product quantities.
Account Suspension: Develop features for suspending and reactivating user accounts.
User Management: Create UI for managing user information and roles.
Reviews and Comments: Implement components for user reviews and comments on products.
Payment Integration: Build interfaces for integrating payment processing with VNPay.

## Technologies Used

### Frontend
- [React](https://reactjs.org/) (v18.2)
- [MUI (Material-UI)](https://mui.com/) (v5.13)
- [TailwindCSS](https://tailwindcss.com/) (v3.1)
- [PostCSS](https://postcss.org/) (v8.4)
- [Emotion](https://emotion.sh/docs/introduction) (v11.11)
- [npm](https://www.npmjs.com/)

### Backend
- [Node.js](https://nodejs.org/)
- [Express](https://expressjs.com/)
- [MongoDB](https://www.mongodb.com/)
- [Mongoose](https://mongoosejs.com/)

## Author
- **Name**: [Your Name]
- **Email**: [your.email@example.com]
- **GitHub**: [Your GitHub Profile](https://github.com/yourusername)

## Installation
To set up the project locally, follow these steps:

### Frontend

1. **Clone the repository**:
    ```sh
    git clone https://github.com/yourusername/your-repo-name.git
    cd your-repo-name/frontend
    ```

2. **Install dependencies**:
    ```sh
    npm install
    ```

3. **Start the development server**:
    ```sh
    npm start
    ```

4. **Build for production**:
    ```sh
    npm run build
    ```

### Backend

1. **Navigate to the backend directory**:
    ```sh
    cd ../backend
    ```

2. **Install dependencies**:
    ```sh
    npm install
    ```

3. **Start the development server**:
    ```sh
    npm start
    ```

## Folder Structure
The project structure is as follows:
your-repo-name/ ├── frontend/ │ ├── public/ │ │ ├── index.html │ │ └── ... │ ├── src/ │ │ ├── assets/ │ │ ├── components/ │ │ ├── pages/ │ │ ├── App.js │ │ ├── index.js │ │ └── ... │ ├── .gitignore │ ├── package.json │ ├── postcss.config.js │ ├── tailwind.config.js │ └── README.md ├── backend/ │ ├── controllers/ │ ├── models/ │ ├── routes/ │ ├── app.js │ ├── server.js │ ├── .gitignore │ ├── package.json │ └── README.md ├── .gitignore ├── README.md └── LICENSE



### Frontend
- **public/**: Contains the static files.
- **src/**: Contains the source code of the application.
  - **assets/**: Contains images, fonts, and other assets.
  - **components/**: Contains reusable React components.
  - **pages/**: Contains the main pages of the application.
- **App.js**: The root component.
- **index.js**: The entry point of the application.
- **postcss.config.js**: Configuration file for PostCSS.
- **tailwind.config.js**: Configuration file for TailwindCSS.
- **package.json**: Contains the project dependencies and scripts.

### Backend
- **controllers/**: Contains the logic for handling requests.
- **models/**: Contains the database schemas.
- **routes/**: Contains the route definitions.
- **app.js**: The main application file.
- **server.js**: The entry point of the backend server.
- **package.json**: Contains the project dependencies and scripts.

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
