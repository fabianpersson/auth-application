import React, { useState, useEffect } from "react";
import { Collection, Card, Heading, Text, Flex } from "@aws-amplify/ui-react";

const fetchData = async (token) => {
  return await fetch(`https://localhost:7271/sessions?token=${token}`, {
    method: "GET",
  }).then(
    (items) => items.json(),
    (error) => []
  );
};

const DefaultCollectionExample = (token) => {
  useEffect(() => {
    console.log("it is " + token);
    // call the async fetchData function
    fetchData(token.token).then((list) => {
      setItems(list);
    });
  }, []);

  const [items, setItems] = useState(["test"]);

  return (
    <Flex direction="column" padding="1em">
      <Heading>Sessions</Heading>
      <Collection type="list" items={items} gap="1.5rem">
        {(item, index) => <Text key={index}>{item}</Text>}
      </Collection>
    </Flex>
  );
};

export default DefaultCollectionExample;
