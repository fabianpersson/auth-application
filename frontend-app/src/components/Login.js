import * as React from "react";

import { useNavigate } from "react-router-dom";

import {
  Button,
  Flex,
  Heading,
  PasswordField,
  TextField,
  useTheme,
  Card,
  Divider,
  Alert,
  Grid,
} from "@aws-amplify/ui-react";

async function loginUser(credentials) {
  return fetch("https://localhost:7271/login", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(credentials),
  }).then(
    (response) => {
      if (response.ok) {
        return response.text();
      } else {
        return "";
      }
    },
    (error) => ""
  );
}

const LoginFormExample = ({ setToken }) => {
  const navigate = useNavigate();
  const { tokens } = useTheme();
  const [loginFailed, setLoginFailed] = React.useState(false);
  const [password, setPassword] = React.useState("");
  const [username, setUsername] = React.useState("");
  return (
    <Grid alignItems="center" justifyContent="center" padding="10rem">
      <Card width="20em" variation="elevated" alignItems="center">
        <Flex
          as="form"
          direction="column"
          gap={tokens.space.medium}
          justifyContent="center"
          alignContent="flex-start"
        >
          <Heading level={3}>Login</Heading>
          <TextField
            label="Username"
            name="username"
            autoComplete="username"
            value={username}
            onChange={(e) => {
              setUsername(e.target.value);
            }}
          />
          <PasswordField
            label="Password"
            name="password"
            autoComplete="current-password"
            value={password}
            onChange={(e) => {
              setPassword(e.target.value);
            }}
          />
          {loginFailed && (
            <Alert variation="error">Invalid username and/or password.</Alert>
          )}
          <Button
            type="submit"
            onClick={async (e) => {
              e.preventDefault();
              const token = await loginUser({
                username,
                password,
              });

              setToken(token);

              if (token !== "") {
                navigate("/sessions");
              } else {
                setLoginFailed(true);
              }
            }}
          >
            Login
          </Button>
          <Divider orientation="horizontal" />
          New here?
          <Button
            onClick={(x) => {
              navigate("/signup");
            }}
          >
            Create Account
          </Button>
        </Flex>
      </Card>
    </Grid>
  );
};

export default LoginFormExample;
