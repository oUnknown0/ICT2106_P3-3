import React from "react";
import { useNavigate } from "react-router";
export default function Logout({ logout,clearPerms }) {
    logout();
    clearPerms();
    const navigate = useNavigate();
    React.useEffect(() => {
        navigate("/");
    })
};