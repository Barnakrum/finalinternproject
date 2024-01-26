import Home from "./components/Home";
import Login from "./components/Auth/Login";
import Register from "./components/Auth/Register";
import ImageUpload from "./components/Utilities/ImageUpload";
import PostCollection from "./components/Collection/PostCollection";
import AllCollections from "./components/Collection/AllCollections";
import AuthRedirect from "./components/Utilities/AuthRedirect";
import CollectionWrapper from "./components/Collection/CollectionWrapper";
import CollectionUser from "./components/Collection/CollectionUser";
import PostItem from "./components/Item/PostItem";
import Item from "./components/Item/Item";
import EditCollection from "./components/Collection/EditCollection";


const AppRoutes = [
    {
        index: true,
        element: <Home />
    },
    // AUTH
    {
        path: '/login',
        element: <Login />
    },
    {
        path: '/register',
        element: <Register />
    },
    {
        path: '/authredirect',
        element: <AuthRedirect />
    },
    // USER
    {
        path: '/user/:UserId',
        element: <CollectionUser />
    },
    // COLLECTION
    {
        path: '/collection/post',
        element: <PostCollection />
    },
    {
        path: '/collection/all',
        element: <AllCollections />
    },
    {
        path: '/collection/:CollectionId',
        element: <CollectionWrapper />
    },
    {
        path: '/collection/edit/:CollectionId',
        element: <EditCollection />
    },
    {
        path: '/upload',
        element: <ImageUpload />
    },
    // ITEM
    {
        path: '/item/post/:CollectionId',
        element: <PostItem isEdit={false}/>
    },
    {
        path: '/item/edit/:CollectionId/:ItemId',
        element: <PostItem isEdit={true}/>
    },
    {
        path: '/item/:CollectionId/:ItemId',
        element: <Item isListMode={false} collectionIdProp={null} itemIdProp={null} />
    },

    {
        path: '/*',
        element: <Home />
    }
    ];

export default AppRoutes;
