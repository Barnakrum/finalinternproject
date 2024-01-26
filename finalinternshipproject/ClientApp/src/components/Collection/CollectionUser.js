import axios from "axios";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import Collection from "./Collection";

export default function CollectionWrapper() {

    const [collection, setCollection] = useState(null)
    
    const params = useParams();
    useEffect(() => {
        try {
            axios.get(process.env.REACT_APP_BASE_URL+"/api/collection/user/"+params.UserId)
            .then((r) => {
            setCollection(r.data.data);

            })
        } catch (error) {
            console.log(error)
        }

    }, [])
   return( collection === null ?
    null
    :
    collection.map((array, index) => (<Collection collectionData={array} variant="short" key={index}/>)))

}