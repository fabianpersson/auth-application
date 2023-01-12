import * as React from "react";

import { useNavigate } from "react-router-dom";

import { Button, useTheme, Card } from "@aws-amplify/ui-react";

const Signout = ({ setToken }) => {
  const { tokens } = useTheme();
  const navigate = useNavigate();

  return (
    <Card width="20em">
      <Button
        onClick={(e) => {
          setToken("");
          navigate("/");
        }}
      >
        Signout
      </Button>
    </Card>
  );
};

export default Signout;
