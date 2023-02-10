import {useState} from 'react';

export default function usePerms(){
    const getPerms = () =>{
        const permsString = sessionStorage.getItem('perms');
        const userPerms = JSON.parse(permsString);
        return userPerms
    };

    const [perms, setPerms] = useState(getPerms());
    const savePerms = userPerms =>{
        sessionStorage.setItem('perms', JSON.stringify(userPerms));
        setPerms(userPerms);
    };

    const clearPerms = () =>{
        console.log("Clearing Perms");
        setPerms(null);
        sessionStorage.removeItem('perms');
    }
    return {
        perms,
        setPerms: savePerms,
        clearPerms: clearPerms
    }
}